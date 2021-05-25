// <copyright file="DeleteUser.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.UseCases.Users
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer.Domain.Models;
    using IdentityServer.PubSub;
    using IdentityServer.Service.Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the <see cref="DeleteUser" /> request.
    /// </summary>
    public sealed record DeleteUser(Guid UserId) : IRequest<bool>;

    /// <summary>
    /// Defines the <see cref="AddUserHandler"/>.
    /// </summary>
    internal sealed class DeleteUserHandler : IRequestHandler<DeleteUser, bool>
    {
        /// <summary>
        /// The ASP.NET Core Identity UserManager, injected via dependency injection.
        /// </summary>
        private readonly UserManager<KwetterUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserHandler" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public DeleteUserHandler(UserManager<KwetterUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Handles the use case for deleteing an existing user.
        /// Handles the use case for deleteing an existing user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>If the user is created succesfully.</returns>
        public async Task<bool> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var foundUser = await this.userManager.FindByIdAsync(request.UserId.ToString());

            if (foundUser == null)
            {
                throw new UserNotFoundException();
            }

            var deleteResult = await this.userManager.DeleteAsync(foundUser);

            await PubSubService.Publish("user-deleted", new
            {
                UserId = foundUser.Id,
                Username = foundUser.UserName,
                Email = foundUser.Email,
            });

            return deleteResult.Succeeded;
        }
    }
}
