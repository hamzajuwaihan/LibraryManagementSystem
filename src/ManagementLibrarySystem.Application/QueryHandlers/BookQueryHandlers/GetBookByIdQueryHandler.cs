using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
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
    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Book? result = await _bookRepository.GetBookById(request.BookId);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}