using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class ReturnBookCommand(Guid bookId) : IRequest<string>
{
    public Guid BookId { get; } = bookId;
}