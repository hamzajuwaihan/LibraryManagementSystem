
using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class PatchBookByIdCommand(Guid bookId, string? title, string? author, bool? isBorrowed, DateTime? borrowedDate, Guid? borrowedBy) : IRequest<Book>
{
    public Guid BookId { get; } = bookId;
    public string? Title { get; } = title;
    public string? Author { get; } = author;
    public bool? IsBorrowed { get; } = isBorrowed;
    public DateTime? BorrowedDate { get; } = borrowedDate;
    public Guid? BorrowedBy { get; } = borrowedBy;
}
