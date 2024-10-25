using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IBookRepository
{
    Task<Book?> GetBookById(Guid id);

    Task<Book?> UpdateBookById(Guid guid, Book book);

    Task<List<Book>> GetAllBooks();

    Task<bool> DeleteBookById(Guid id);

    Task<Book> CreateBook(Book book);

}
