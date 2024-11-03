using System;
using FluentValidation;
using ManagementLibrarySystem.Application.Commands.MemberCommands;

namespace ManagementLibrarySystem.Presentation.Api.Validators;

public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
{

    public AddMemberCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotNull()
            .WithMessage("Name is required.")
            .NotEmpty()
            .WithMessage("Name must not be empty.")
            .Length(1, 100)
            .WithMessage("Name must be between 1 and 100 characters long.");

        RuleFor(command => command.Email)
            .NotNull()
            .WithMessage("Email is required.")
            .NotEmpty()
            .WithMessage("Email must not be empty.")
            .EmailAddress()
            .WithMessage("Email format is not valid.")
            .Length(5, 255)
            .WithMessage("Email must be between 5 and 255 characters long.");
    }
}
