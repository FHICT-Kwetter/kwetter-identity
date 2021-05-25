// <copyright file="EmailAlreadyTakenException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.Exceptions
{
    using System;
    using IdentityServer.Service.Exceptions.Base;

    /// <summary>
    /// Defines the <see cref="EmailAlreadyTakenException" />.
    /// </summary>
    [Serializable]
    public class EmailAlreadyTakenException : KwetterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAlreadyTakenException"/> class.
        /// </summary>
        public EmailAlreadyTakenException() : base("This email is already in use!")
        {
        }
    }
}
