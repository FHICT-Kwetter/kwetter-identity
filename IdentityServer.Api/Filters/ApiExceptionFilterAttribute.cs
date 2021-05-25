// <copyright file="ApiExceptionFilterAttribute.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Api.Filters
{
    using System;
    using IdentityServer.Api.Contracts.Responses;
    using IdentityServer.Service.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Defines the <see cref="ApiExceptionFilterAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Defines how an error should be resolved into the http result.
        /// </summary>
        /// <param name="context">The context of the exception.</param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EmailAlreadyTakenException:
                case UsernameAlreadyTakenException:
                    context.Result = new BadRequestObjectResult(new ApiExceptionResponse() { Message = context.Exception.Message });
                    return;
                case UserNotFoundException:
                    context.Result = new NotFoundObjectResult(new ApiExceptionResponse() { Message = context.Exception.Message });
                    return;
            }
        }
    }
}