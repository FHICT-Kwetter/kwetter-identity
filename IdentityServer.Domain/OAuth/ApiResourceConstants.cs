// <copyright file="ApiResourceConstants.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.OAuth
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    /// <summary>
    /// Defines the api resource scopes.
    /// </summary>
    public static class ApiResourceConstants
    {
        /// <summary>
        /// The kweet api scopes.
        /// </summary>
        public static readonly ApiResource KwetterApiResource = new("kwetter-web-app", "The kwetter webapp")
        {
            Scopes = { "kweet.list", "kweet.read", "kweet.add", "kweet.delete" },
        };

        /// <summary>
        /// Gets all the registered clients.
        /// </summary>
        /// <returns>A list of all registered clients.</returns>
        public static IEnumerable<ApiResource> GetAll()
        {
            return new List<ApiResource>()
            {
                KwetterApiResource,
            };
        }
    }
}