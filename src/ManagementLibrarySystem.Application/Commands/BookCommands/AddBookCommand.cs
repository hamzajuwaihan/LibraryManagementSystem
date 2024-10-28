using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class AddBookCommand(string title, string author, Guid libraryId) : IRequest<Book>
{
    public string Title { get; } = title;
    public string Author { get; } = author;
    public Guid LibraryId { get; } = libraryId;

}