// <copyright file="UsernameAlreadyTakenException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.Exceptions
{
    using System;
    using IdentityServer.Service.Exceptions.Base;

    /// <summary>
    /// Defines the <see cref="UsernameAlreadyTakenException" />.
    /// </summary>
    [Serializable]
    public class UsernameAlreadyTakenException : KwetterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameAlreadyTakenException"/> class.
        /// </summary>
        public UsernameAlreadyTakenException() : base("This username is already in use!")
        {
        }
    }
}
