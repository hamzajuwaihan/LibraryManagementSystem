using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Validators;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Book ID is required")
            .Must(BeAValidGuid).WithMessage("Book ID must be a valid GUID");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
