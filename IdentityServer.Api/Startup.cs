// <copyright file="Startup.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

using IdentityServer.PubSub;

namespace IdentityServer.Api
{
    using System;
    using FluentValidation.AspNetCore;
    using IdentityServer.Api.Contracts.Requests;
    using IdentityServer.Data.Contexts;
    using IdentityServer.Data.Extensions;
    using IdentityServer.Domain.Models;
    using IdentityServer.Domain.OAuth;
    using IdentityServer.Domain.Services;
    using IdentityServer.Service.UseCases.Users;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="Startup" />.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            services.AddControllers();

            services.AddDatabaseLayer(this.configuration);

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(AddUser));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = env == "Production" ? "https://www.kwetter.org/api/auth" : "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                    };
                    options.MapInboundClaims = false;
                });
            services.AddAuthorization();

            services.Configure<IdentityOptions>(options =>
            {
                // User username configuration
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true;

                // User password configuration
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                // Lockout configuration (When users fail logging in too many times in a row)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

            services.AddIdentity<KwetterUser, KwetterRole>()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<KwetterUser>()
                .AddInMemoryPersistedGrants()
                .AddInMemoryClients(ClientConstants.GetAll())
                .AddInMemoryIdentityResources(IdentityResourceConstants.GetAll())
                .AddInMemoryApiScopes(ApiScopeConstants.GetAll())
                .AddInMemoryApiResources(ApiResourceConstants.GetAll())
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            services.AddHealthChecks();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
            }

            app.UseForwardedHeaders();
            app.UsePathBase(new PathString("/api/auth"));
            app.UseHealthChecks("/api/auth/health");
            app.UseHealthChecks("/health");
            app.UseCors(x => x.WithOrigins("https://kwetter.org", "https://www.kwetter.org").AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
