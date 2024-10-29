using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Abstractions;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
{
    /// <summary>
    /// Define business validation for borrow book command
    /// </summary>
    public BorrowBookCommandValidator()
    {
        RuleFor(command => command.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .Must(BeAValidGuid).WithMessage("Book ID must be a valid GUID");

        RuleFor(command => command.MemberId)
            .NotEmpty().WithMessage("Member ID is required")
            .Must(BeAValidGuid).WithMessage("Member ID must be a valid GUID");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
