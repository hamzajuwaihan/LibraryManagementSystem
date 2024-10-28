using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class BorrowBookCommand(Guid bookId, Guid memberId) : IRequest<string>
{

    public Guid BookId { get; } = bookId;
    public Guid MemberId { get; } = memberId;

}
