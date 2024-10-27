
using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Abstractions;

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    /// <summary>
    /// Define business validation for add book command
    /// </summary>
    public AddBookCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");

        RuleFor(command => command.Author)
            .NotEmpty().WithMessage("Author is required");

    }
}
