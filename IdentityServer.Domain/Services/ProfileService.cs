// <copyright file="ProfileService.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityModel;
    using IdentityServer.Domain.Models;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Implementation of the profile service.
    /// https://docs.identityserver.io/en/latest/reference/profileservice.html#refprofileservice
    /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// The ASP.Net Identity UserManager.
        /// </summary>
        private readonly UserManager<KwetterUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService"/> class.
        /// </summary>
        /// <param name="userManager">The ASP.Net Identity UserManager.</param>
        public ProfileService(UserManager<KwetterUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// The API that is expected to load claims for a user.
        /// </summary>
        /// <param name="context">The <see cref="ProfileDataRequestContext"/>.</param>
        /// <returns>The list of claims added into the response.</returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await this.userManager.GetUserAsync(context.Subject);

            // Add email
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));

            // Add audience
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Audience, "kwetter-web-app"));

            // Add roles
            foreach (var role in await this.userManager.GetRolesAsync(user))
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }

        /// <summary>
        /// The API that is expected to indicate if a user is currently allowed to obtain tokens.
        /// </summary>
        /// <param name="context">The <see cref="IsActiveAsync"/>.</param>
        /// <returns>True if the user is allowed to obtain tokens, otherewise false.</returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await this.userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}