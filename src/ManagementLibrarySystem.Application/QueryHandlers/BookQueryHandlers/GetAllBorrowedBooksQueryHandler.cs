using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;

public class GetAllBorrowedBooksQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetAllBorrowedBooksQuery, List<Book>>
{
    private readonly IBookRepository _bookRepository = bookRepository;


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
            throw;
        }
    }
}
