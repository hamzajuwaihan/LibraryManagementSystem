using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IBookRepository
{
    Task<Book?> GetBookById(Guid id);

    Task<Book?> UpdateBookById(Guid guid, Book book);

    Task<IEnumerable<Book>> GetAllBooks();

    Task<bool> DeleteBookById(Guid id);

    Task<Book> AddBook(Book book);

    Task<List<Book>> GetAllBorrowedBooks();
}
