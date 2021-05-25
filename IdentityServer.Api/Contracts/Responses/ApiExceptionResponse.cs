// <copyright file="ApiExceptionResponse.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Api.Contracts.Responses
{
    using System;

    /// <summary>
    /// Defines the <see cref="ApiExceptionResponse" />.
    /// </summary>
    public class ApiExceptionResponse
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }
    }
}