// <copyright file="ResourceOwnerPasswordValidator.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.Services
{
    using System.Threading.Tasks;
    using IdentityServer.Domain.Models;
    using IdentityServer4.Models;
    using IdentityServer4.Validation;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Implementation of the resource owner password validator.
    /// https://identityserver4.readthedocs.io/en/latest/topics/resource_owner.html
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// The ASP.Net Identity UserManager.
        /// </summary>
        private readonly UserManager<KwetterUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceOwnerPasswordValidator"/> class.
        /// </summary>
        /// <param name="userManager">The ASP.Net Identity UserManager.</param>
        public ResourceOwnerPasswordValidator(UserManager<KwetterUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Implements the logic which is executed on calling the API with the resource owner password flow.
        /// </summary>
        /// <param name="context">The <see cref="ResourceOwnerPasswordValidationContext"/>.</param>
        /// <returns>An awaitable task.</returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            KwetterUser user;

            if (context.UserName.Contains("@"))
            {
                user = await this.userManager.FindByEmailAsync(context.UserName);
            }
            else
            {
                user = await this.userManager.FindByNameAsync(context.UserName);
            }

            if (user == null || !await this.userManager.CheckPasswordAsync(user, context.Password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
                return;
            }

            context.Result = new GrantValidationResult(user.Id.ToString(), "password");
        }
    }
}