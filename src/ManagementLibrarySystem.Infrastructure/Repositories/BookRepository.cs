using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class BookRepository(DbAppContext context) : IBookRepository
{
    private readonly DbAppContext _context = context;
    public async Task<Book> CreateBook(Book book)
    {

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookById(Guid id)
    {

        Book? book = await GetBookById(id) ?? throw new BookNotFoundException();

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Book> GetAllBooks() => _context.Books;

    public IQueryable<Book> GetAllBorrowedBooks() => _context.Books;

    public async Task<Book?> GetBookById(Guid id) => await _context.Books.FirstOrDefaultAsync(book => book.Id == id);

    public async Task<Book?> PatchBookById(Guid id, Book book)
    {
        Book? existingBook = await GetBookById(id) ?? throw new BookNotFoundException();

        existingBook.Update(
            title: !string.IsNullOrEmpty(book.Title) ? book.Title : existingBook.Title,
            author: !string.IsNullOrEmpty(book.Author) ? book.Author : existingBook.Author,
            isBorrowed: book.IsBorrowed,
            borrowedDate: book.BorrowedDate ?? existingBook.BorrowedDate,
            borrowedBy: book.BorrowedBy ?? existingBook.BorrowedBy
        );

        await _context.SaveChangesAsync();
        return existingBook;
    }

    public async Task<Book?> UpdateBookById(Guid id, Book book)
    {
        Book? originalBook = await _context.Books.FindAsync(id) ?? throw new BookNotFoundException();

        originalBook.Update(book.Title, book.Author, book.IsBorrowed, book.BorrowedDate, book.BorrowedBy);

        await _context.SaveChangesAsync();

        return originalBook;
    }
}
