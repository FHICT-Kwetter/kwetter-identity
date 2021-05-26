// <copyright file="ServiceCollectionExtensions.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.Extensions
{
    using System;
    using IdentityServer.Data.Contexts;
    using IdentityServer.Domain.Models;
    using IdentityServer.Domain.OAuth;
    using IdentityServer.Domain.Services;
    using IdentityServer.Service.UseCases.Users;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines extension methods on the <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the api layer to the application's service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(AddUser));

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
        }
    }
}