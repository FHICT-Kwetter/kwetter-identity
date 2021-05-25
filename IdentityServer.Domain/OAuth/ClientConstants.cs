// <copyright file="ClientConstants.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.OAuth
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    /// <summary>
    /// This static class contains all the registered in-memory <see cref="Client"/> objects.
    /// </summary>
    public static class ClientConstants
    {
        /// <summary>
        /// The kwetter web application.
        /// </summary>
        public static readonly Client KwetterWebApp = new Client()
        {
            ClientId = "kwetter-web-app",
            ClientSecrets = new List<Secret> { new Secret("kwetter-web-app-secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = new List<string>
            {
                IdentityResourceConstants.OpenIdScope.Name,
                IdentityResourceConstants.ProfileScope.Name,
                IdentityResourceConstants.EmailScope.Name,
                "kweet.list",
            },
            AllowOfflineAccess = true,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Absolute,
            UpdateAccessTokenClaimsOnRefresh = true,
        };

        /// <summary>
        /// Gets all the registered clients.
        /// </summary>
        /// <returns>A list of all registered clients.</returns>
        public static IEnumerable<Client> GetAll()
        {
            return new List<Client>()
            {
                KwetterWebApp,
            };
        }
    }
}