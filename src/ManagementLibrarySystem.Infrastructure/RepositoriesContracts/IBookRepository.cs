using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IBookRepository
{
    Task<Book?> GetBookById(Guid id);

    Task<Book?> UpdateBookById(Guid guid, Book book);

    IQueryable<Book> GetAllBooks();

    Task<bool> DeleteBookById(Guid id);

    Task<Book> CreateBook(Book book);

    IQueryable<Book> GetAllBorrowedBooks();

    Task<Book?> PatchBookById(Guid id, Book book);
}
