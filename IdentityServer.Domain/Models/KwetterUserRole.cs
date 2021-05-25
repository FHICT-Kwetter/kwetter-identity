// <copyright file="KwetterUserRole.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Domain.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the join model between users and roles.
    /// </summary>
    public class KwetterUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Gets or sets the <see cref="KwetterUser"/>.
        /// </summary>
        public virtual KwetterUser User { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="KwetterRole"/>.
        /// </summary>
        public virtual KwetterRole Role { get; set; }
    }
}
