using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Validators;

public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
{
    /// <summary>
    /// Define business validation for borrow book command
    /// </summary>
    public BorrowBookCommandValidator()
    {
        RuleFor(command => command.MemberId)
            .NotEmpty().WithMessage("Member ID is required")
            .Must(BeAValidGuid).WithMessage("Member ID must be a valid GUID");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
