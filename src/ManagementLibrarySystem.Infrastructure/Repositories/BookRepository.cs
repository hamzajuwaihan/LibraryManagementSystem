using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Domain.Exceptions.Library;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class BookRepository(DbAppContext context, IMemberRepository memberRepository) : IBookRepository
{
    private readonly DbAppContext _context = context;
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<Book> CreateBook(Book book)
    {

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBook(Guid id)
    {

        Book? book = await GetBookById(id) ?? throw new BookNotFoundException();

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Book>> GetAllBooks(int pageSize, int pageNumber)
    {
        int skip = (pageNumber - 1) * pageSize;

        return await _context.Books
            .OrderBy(b => b.Title)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<List<Book>> GetAllBooks() => await _context.Books.ToListAsync();


    public async Task<List<Book>> GetAllBorrowedBooks(int pageSize, int pageNumber)
    {
        int skip = (pageNumber - 1) * pageSize;

        return await _context.Books
            .Where(b => b.IsBorrowed == true)
            .OrderBy(b => b.Title)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<List<Book>> GetAllBorrowedBooks() => await _context.Books.Where(b => b.IsBorrowed == true).ToListAsync();

    public async Task<Book> GetBookById(Guid id) => await _context.Books.FirstOrDefaultAsync(book => book.Id == id) ?? throw new BookNotFoundException();

    public async Task<Book> PatchBook(Guid id, Book book)
    {
        Book existingBook = await GetBookById(id) ?? throw new BookNotFoundException();

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

    public async Task<Book> BorrowBook(Guid bookId, Guid memberId)
    {

        Book book = await GetBookById(bookId) ?? throw new BookNotFoundException();

        Member member = await _memberRepository.GetMemberById(memberId) ?? throw new MemberNotFoundException();


        if (book.IsBorrowed) throw new BookAlreadyBorrowedException();

        Library library = await _context.Libraries
                                .Include(l => l.Members)
                                .FirstOrDefaultAsync(l => l.Id == book.LibraryId)
                                ?? throw new LibraryNotFoundException();

        if (!library.Members.Any(m => m.Id == member.Id)) throw new LibraryDoesNotHaveThisMemberException();
        
        book.IsBorrowed = true;
        book.BorrowedBy = member.Id;
        book.BorrowedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return book;
    }
    public async Task<Book> ReturnBook(Guid id)
    {
        Book book = await GetBookById(id);

        if (!book.IsBorrowed) throw new BookIsNotCurrentlyBorrowedException();

        book.BorrowedDate = null;
        book.BorrowedBy = null;
        book.IsBorrowed = false;

        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateBook(Guid id, Book book)
    {
        Book? originalBook = await _context.Books.FindAsync(id) ?? throw new BookNotFoundException();

        originalBook.Update(book.Title, book.Author, book.IsBorrowed, book.BorrowedDate, book.BorrowedBy);

        await _context.SaveChangesAsync();

        return originalBook;
    }
}
