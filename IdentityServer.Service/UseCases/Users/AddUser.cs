// <copyright file="AddUser.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Service.UseCases.Users
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using IdentityServer.Domain.Models;
    using IdentityServer.PubSub;
    using IdentityServer.Service.Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Defines the <see cref="AddUser" /> request.
    /// </summary>
    public sealed record AddUser(string Username, string Email, string Password) : IRequest<bool>;

    /// <summary>
    /// Defines the <see cref="AddUserHandler"/>.
    /// </summary>
    internal sealed class AddUserHandler : IRequestHandler<AddUser, bool>
    {
        /// <summary>
        /// The ASP.NET Core Identity UserManager, injected via dependency injection.
        /// </summary>
        private readonly UserManager<KwetterUser> userManager;

        /// <summary>
        /// The ASP.NET Core Identity RoleManager, injected via dependency injection.
        /// </summary>
        private readonly RoleManager<KwetterRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserHandler" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        public AddUserHandler(UserManager<KwetterUser> userManager, RoleManager<KwetterRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Handles the use case for adding a new user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>If the user is created succesfully.</returns>
        public async Task<bool> Handle(AddUser request, CancellationToken cancellationToken)
        {
            var (username, email, password) = request;

            if (await this.userManager.FindByEmailAsync(email) != null)
            {
                throw new EmailAlreadyTakenException();
            }

            if (await this.userManager.FindByNameAsync(username) != null)
            {
                throw new UsernameAlreadyTakenException();
            }

            var user = new KwetterUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = username,
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var userRole = await this.roleManager.FindByNameAsync("user");

            if (userRole == null)
            {
                await this.roleManager.CreateAsync(new KwetterRole() { Name = "user", Description = "The user role." });
            }

            await this.userManager.AddToRoleAsync(user, "user");

            await PubSubService.Publish("user-created", new
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
            });

            return result.Succeeded;
        }
    }
}
