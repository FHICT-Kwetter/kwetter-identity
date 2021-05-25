// <copyright file="ApiScopeConstants.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.OAuth
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    /// <summary>
    /// Defines the specefic api scopes that can be requested through oauth.
    /// </summary>
    public static class ApiScopeConstants
    {
        /// <summary>
        /// Scopes to list kweets.
        /// </summary>
        public static readonly ApiScope KweetList = new ApiScope("kweet.list");

        /// <summary>
        /// Scopes to read kweets.
        /// </summary>
        public static readonly ApiScope KweetRead = new ApiScope("kweet.read");

        /// <summary>
        /// Scopes to add kweets.
        /// </summary>
        public static readonly ApiScope KweetAdd = new ApiScope("kweet.add");

        /// <summary>
        /// Scopes to delete kweets.
        /// </summary>
        public static readonly ApiScope KweetDelete = new ApiScope("kweet.delete");

        /// <summary>
        /// Gets all the api scopes.
        /// </summary>
        /// <returns>A list of all api scopes.</returns>
        public static IEnumerable<ApiScope> GetAll()
        {
            return new[]
            {
                KweetList, KweetRead, KweetAdd, KweetDelete,
            };
        }
    }
}