// <copyright file="UserNotFoundException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.Exceptions
{
    using System;
    using IdentityServer.Service.Exceptions.Base;

    /// <summary>
    /// Defines the <see cref="UserNotFoundException" />.
    /// </summary>
    [Serializable]
    public class UserNotFoundException : KwetterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException"/> class.
        /// </summary>
        public UserNotFoundException() : base("The user was not found!")
        {
        }
    }
}