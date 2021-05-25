// <copyright file="CreateUserRequest.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

namespace IdentityServer.Api.Contracts.Requests
{
    using System;
    using FluentValidation;

    /// <summary>
    /// Defines the <see cref="CreateUserRequest" />.
    /// </summary>
    [Serializable]
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Defines the validator for the <see cref="CreateUserRequest"/>.
    /// </summary>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserRequestValidator"/> class.
        /// </summary>
        public CreateUserRequestValidator()
        {
            // Email rules
            this.RuleFor(req => req.Email).NotNull().NotEmpty().WithMessage("Email must be entered.");
            this.RuleFor(req => req.Email).EmailAddress().WithMessage("Email must be a correct email.");

            // Username rules
            this.RuleFor(req => req.Username).NotNull().NotEmpty().WithMessage("Username cannot be null or empty.");
            this.RuleFor(req => req.Username).MinimumLength(4).WithMessage("Username cannot contain less than 4 characters");
            this.RuleFor(req => req.Username).MaximumLength(15).WithMessage("Username cannot container more than 15 characters");

            // Password rules
            this.RuleFor(req => req.Password).NotNull().NotEmpty().WithMessage("Password must be entered");
            this.RuleFor(req => req.Password).NotNull().NotEmpty().WithMessage("Password must be at least 6 characters.");
        }
    }
}
