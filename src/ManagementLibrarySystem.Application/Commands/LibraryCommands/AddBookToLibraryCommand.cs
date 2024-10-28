using MediatR;

namespace ManagementLibrarySystem.Application.Commands.LibraryCommands;

public class AddBookToLibraryCommand(Guid libraryId, Guid bookId) : IRequest<bool>
{
    public Guid LibraryId { get; } = libraryId;
    public Guid BookId { get; } = bookId;
}
