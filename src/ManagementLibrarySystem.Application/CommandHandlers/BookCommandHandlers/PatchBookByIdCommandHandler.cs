
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Implementation for PatchBookByIdCommand Handler function for MediatR
/// </summary>
/// <param name="bookRepository"></param>
public class PatchBookByIdCommandHandler(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<PatchBookByIdCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Handle function to receive PatchBookById
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<Book> Handle(PatchBookByIdCommand request, CancellationToken cancellationToken)
    {
        string? id = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (id == null || !Guid.TryParse(id, out Guid bookId)) throw new ArgumentException("Invalid or missing 'id' in route.");
        

        Book? book = await _bookRepository.GetBookById(bookId) ?? throw new BookNotFoundException();

        book.Update(
            title: request.Title ?? book.Title,
            author: request.Author ?? book.Author,
            isBorrowed: request.IsBorrowed ?? book.IsBorrowed,
            borrowedDate: request.BorrowedDate ?? book.BorrowedDate,
            borrowedBy: request.BorrowedBy ?? book.BorrowedBy
        );

        await _bookRepository.PatchBookById(bookId, book);

        return book;
    }
}
