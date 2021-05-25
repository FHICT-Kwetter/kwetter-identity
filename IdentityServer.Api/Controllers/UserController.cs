// <copyright file="UserController.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using IdentityServer.Api.Contracts.Requests;
    using IdentityServer.Api.Filters;
    using IdentityServer.Service.UseCases.Users;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the <see cref="UserController" />.
    /// </summary>
    [ApiController]
    [ApiExceptionFilter]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("users")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Defines the <see cref="IMediator"/>.
        /// The Mediator is used as a gateway for controllers to call logic in the use case layer.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/> injected via dependency injection.</param>
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Adds a new user account to the Kwetter platform.
        /// </summary>
        /// <param name="requestBody">The HTTP Request Body model.</param>
        /// <returns code="200">Ok - The new user account was succesfully created.</returns>
        /// <returns code="400">Bad Request - Something went wrong while creating the new user account.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequest requestBody)
        {
            var result = await this.mediator.Send(new AddUser(requestBody.Username, requestBody.Email, requestBody.Password));
            return this.Ok(result);
        }

        /// <summary>
        /// Find the user who makes this request and delete all of his data with his account from the Kwetter platform.
        /// </summary>
        /// <returns code="204">No Content - There is no response body for this request.</returns>
        /// <returns code="401">Unauthorized - The provided Bearer Token was not accepted.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteSelf()
        {
            var userId = this.User.Claims.First(x => x.Type == "sub").Value;
            var result = await this.mediator.Send(new DeleteUser(Guid.Parse(userId)));

            return this.Ok(result);
        }
    }
}
