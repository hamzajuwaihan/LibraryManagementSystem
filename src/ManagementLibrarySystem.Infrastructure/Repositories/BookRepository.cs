using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class BookRepository(DbAppContext context) : IBookRepository
{
    private readonly DbAppContext _context = context;
    public async Task<Book> AddBook(Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteBookById(Guid id)
    {
        try
        {
            Book? book = await GetBookById(id);

            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<Book>> GetAllBooks() => await _context.Books.ToListAsync();

    public async Task<List<Book>> GetAllBorrowedBooks() => await _context.Books.Where(book => book.IsBorrowed).ToListAsync();
    
    public async Task<Book?> GetBookById(Guid id) => await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
    

    public async Task<Book?> UpdateBookById(Guid id, Book book)
    {
        Book? existingBook = await _context.Books.FindAsync(id);

        if (existingBook == null) return null;

        existingBook.Update(book.Title, book.Author, book.IsBorrowed, book.BorrowedDate, book.BorrowedBy);

        await _context.SaveChangesAsync();

        return existingBook;
    }
}
