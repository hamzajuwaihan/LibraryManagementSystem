using FluentValidation;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Presentation.Api.Abstractions;


public class PatchBookByIdCommandValidator : AbstractValidator<PatchBookByIdCommand>
{
    public PatchBookByIdCommandValidator()
    {
        RuleFor(command => command.BookId)
            .NotEmpty().WithMessage("Book ID is required")
            .Must(guid => BeAValidGuid(guid)).WithMessage("Book ID must be a valid GUID");

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
