// <copyright file="AspNetCoreIdentityContext.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Data.Contexts
{
    using System;
    using IdentityServer.Domain.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the ASP.NET Core Identity Context.
    ///
    /// This context contains:
    ///     - ASP.NET Core Identity Users
    ///     - ASP.NET Core Identity User Claims
    ///     - ASP.NET Core Identity User Roles
    ///     - ASP.NET Core Identity User Logins
    ///     - ASP.NET Core Identity User Tokens
    ///     - ASP.NET Core Identity Roles
    ///     - ASP.NET Core Identity Role Claims
    /// </summary>
    public sealed class AspNetCoreIdentityContext : IdentityDbContext<
            KwetterUser,
            KwetterRole,
            Guid,
            IdentityUserClaim<Guid>,
            KwetterUserRole,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>
        >
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspNetCoreIdentityContext"/> class.
        /// </summary>
        /// <param name="options">The database context options objects.</param>
        public AspNetCoreIdentityContext(DbContextOptions<AspNetCoreIdentityContext> options) : base(options)
        {
        }

        /// <summary>
        /// Defines the rules and relationships of database models.
        /// </summary>
        /// <param name="builder">The <see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("IdentityServer");

            builder.Entity<KwetterUser>(user =>
            {
                user.HasMany(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(ur => ur.UserId).IsRequired();
            });
        }
    }
}
