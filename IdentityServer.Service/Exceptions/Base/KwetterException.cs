// <copyright file="KwetterException.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.Exceptions.Base
{
    using System;

    /// <summary>
    /// Defines the <see cref="KwetterException" />.
    /// </summary>
    [Serializable]
    public class KwetterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KwetterException"/> class.
        /// Creates an <see cref="KwetterException"/> with a set message.
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        protected KwetterException(string message) : base(message)
        {
        }
    }
}
