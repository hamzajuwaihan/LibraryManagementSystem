using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
/// <summary>
/// Get All borrowed books query handler class
/// </summary>
/// <param name="bookRepository"></param>
public class GetAllBorrowedBooksQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetAllBorrowedBooksQuery, List<Book>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    /// <summary>
    /// Handle function to return all borrowed books
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Book>> Handle(GetAllBorrowedBooksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Book> borrowedBooks = await _bookRepository.GetAllBorrowedBooks();
            return borrowedBooks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            return [];
        }
    }
}
