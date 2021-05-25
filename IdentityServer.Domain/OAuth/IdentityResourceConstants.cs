// <copyright file="IdentityResourceConstants.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.OAuth
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    /// <summary>
    /// This static class contains all the registered in-memory <see cref="IdentityResource"/> objects.
    /// </summary>
    public static class IdentityResourceConstants
    {
        /// <summary>
        /// Defines the OpenId scope. This scope tells the provider to return the sub claim in the identity token.
        /// This sub claim defines the subject id (user id).
        /// </summary>
        public static readonly IdentityResource OpenIdScope = new IdentityResources.OpenId();

        /// <summary>
        /// Defines the Profile scope. The profile scope defines general information about the user.
        /// </summary>
        public static readonly IdentityResource ProfileScope = new IdentityResources.Profile();

        /// <summary>
        /// Defines the Email scope. This returns the email claim with the users email and whether the email is
        /// verified or not.
        /// </summary>
        public static readonly IdentityResource EmailScope = new IdentityResources.Email();

        /// <summary>
        /// Gets all the identity resources
        /// </summary>
        /// <returns>A list of all the identity resources.</returns>
        public static IEnumerable<IdentityResource> GetAll()
        {
            return new List<IdentityResource>
            {
                OpenIdScope,
                ProfileScope,
                EmailScope,
            };
        }
    }
}