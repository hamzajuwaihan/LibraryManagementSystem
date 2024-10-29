
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Implementation for PatchBookByIdCommand Handler function for MediatR
/// </summary>
/// <param name="bookRepository"></param>
public class PatchBookByIdCommandHandler(IBookRepository bookRepository) : IRequestHandler<PatchBookByIdCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handle function to receive PatchBookById
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<Book> Handle(PatchBookByIdCommand request, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetBookById(request.BookId) ?? throw new KeyNotFoundException("Book not found");

        book.Update(
            title: request.Title ?? book.Title,
            author: request.Author ?? book.Author,
            isBorrowed: request.IsBorrowed ?? book.IsBorrowed,
            borrowedDate: request.BorrowedDate ?? book.BorrowedDate,
            borrowedBy: request.BorrowedBy ?? book.BorrowedBy
        );

        await _bookRepository.PatchBookById(request.BookId, book);

        return book;
    }
}
