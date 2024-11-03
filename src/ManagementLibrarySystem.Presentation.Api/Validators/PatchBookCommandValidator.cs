using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Validators;


public class PatchBookByIdCommandValidator : AbstractValidator<PatchBookCommand>
{
    public PatchBookByIdCommandValidator()
    {
        RuleFor(command => command.BorrowedBy)
            .Must(guid => guid == null || BeAValidGuid(guid.Value)).WithMessage("Borrowed By must be a valid GUID")
            .When(command => command.BorrowedBy.HasValue);

        RuleFor(command => command.BorrowedDate)
            .Must(date => date == null || BeAValidDate(date.Value)).WithMessage("Borrowed Date must be a valid date")
            .When(command => command.BorrowedDate.HasValue);
    }

    private bool BeAValidGuid(Guid guid) => guid != Guid.Empty;

    private bool BeAValidDate(DateTime date) => date <= DateTime.UtcNow;
}
