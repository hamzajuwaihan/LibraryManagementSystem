
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Implementation for PatchBookCommand Handler function for MediatR
/// </summary>
/// <param name="bookRepository"></param>
public class PatchBookCommandHandler(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<PatchBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Handle function to receive PatchBook
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<Book> Handle(PatchBookCommand request, CancellationToken cancellationToken)
    {
        Guid id = Guid.Parse(_httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString()!);

        Book book = await _bookRepository.GetBookById(id);

        book.Update(
            title: request.Title ?? book.Title,
            author: request.Author ?? book.Author,
            isBorrowed: request.IsBorrowed ?? book.IsBorrowed,
            borrowedDate: request.BorrowedDate ?? book.BorrowedDate,
            borrowedBy: request.BorrowedBy ?? book.BorrowedBy
        );

        await _bookRepository.PatchBook(id, book);

        return book;
    }
}
