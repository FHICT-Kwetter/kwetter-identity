// <copyright file="KwetterUser.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the custom user for kwetter, which extends from the identity user class with Guid as the primary key type.
    /// </summary>
    public class KwetterUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Navigation property to access user roles via the kwetter user object.
        /// Because of the many-to-many relation between users and roles, the navigation property is a join model.
        /// </summary>
        public virtual ICollection<KwetterUserRole> UserRoles { get; set; }
    }
}
