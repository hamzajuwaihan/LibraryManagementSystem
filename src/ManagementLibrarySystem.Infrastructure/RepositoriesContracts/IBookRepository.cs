using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IBookRepository
{
    Task<Book> GetBookById(Guid id);

    Task<Book?> UpdateBook(Guid guid, Book book);

    Task<List<Book>> GetAllBooks(int pageSize, int pageNumber);
    Task<List<Book>> GetAllBooks();
    Task<bool> DeleteBookById(Guid id);

    Task<Book> CreateBook(Book book);
    Task<List<Book>> GetAllBorrowedBooks(int pageSize, int pageNumber);
    Task<List<Book>> GetAllBorrowedBooks();
    Task<Book?> PatchBookById(Guid id, Book book);
}
