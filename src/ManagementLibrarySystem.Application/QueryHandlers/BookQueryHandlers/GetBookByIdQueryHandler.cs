using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
/// <summary>
/// Get book by Id Handler class
/// </summary>
/// <param name="bookRepository"></param>
public class GetBookByIdQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handle fucntion implementation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken) => await _bookRepository.GetBookById(request.Id) ?? throw new BookNotFoundException();

}