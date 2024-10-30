using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class BorrowBookCommand( Guid memberId) : IRequest<string>
{

    public Guid MemberId { get; } = memberId;

}
