// <copyright file="KwetterRole.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the kwetter role model.
    /// </summary>
    public class KwetterRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the join model to navigate to the user of the role.
        /// </summary>
        public virtual ICollection<KwetterUserRole> UserRoles { get; set; }
    }
}
