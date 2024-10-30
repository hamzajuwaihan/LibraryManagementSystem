using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
/// <summary>
/// Query Handler
/// </summary>
/// <param name="bookRepository"></param>
public class GetAllBooksQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetAllBooksQuery, List<Book>>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handle implemetnation returns all books in DB
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.PageNumber - 1) * request.PageSize;

        List<Book> books = await _bookRepository
            .GetAllBooks()
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync();

        return books;
    }

}
