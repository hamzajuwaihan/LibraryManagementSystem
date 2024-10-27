using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ManagementLibrarySystem.Infastructure.Test;

public class BookRepositoryTests
{
    private DbAppContext CreateDbContext()
    {
        DbContextOptions<DbAppContext> options = new DbContextOptionsBuilder<DbAppContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DbAppContext(options);
    }
    #region AddingBookTests
    [Fact]
    public async Task CreateBook_ShouldAddBookToDatabase()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Book book = new(Guid.NewGuid())
        {
            Title = "Sample Book",
            Author = "Author Name",
        };

        Book createdBook = await repository.AddBook(book);

        Assert.NotNull(createdBook);
        Assert.Equal("Sample Book", createdBook.Title);
        Assert.Equal("Author Name", createdBook.Author);
        Assert.Single(context.Books);
    }

    [Fact]
    public async Task CreateBook_ShouldReturnExceptionWhenBookIsNull()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Book newBook = null!;

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddBook(newBook));

    }
    #endregion

    #region  GetBookTests
    [Fact]
    public async Task GetBookById_ShouldReturnBookSuccessfuly()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);


        Guid bookId = Guid.NewGuid();

        Book book = new(bookId)
        {
            Title = "Sample Book",
            Author = "Author Name",
        };


        Book createdBook = await repository.AddBook(book);
        Book? searchBook = await repository.GetBookById(createdBook.Id);


        Assert.Equal(createdBook.Id, searchBook?.Id);
        Assert.Equal(createdBook.Title, searchBook?.Title);
        Assert.Equal(createdBook.Author, searchBook?.Author);
    }

    //TODO: Add a test for getting all books

    #endregion
    [Fact]
    public async Task GetBookById_ShouldReturnNullWhenBookNotFound()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Guid randomGuid = Guid.NewGuid();

        Book? searchBook = await repository.GetBookById(randomGuid);

        Assert.Null(searchBook);

    }

    #region DeleteBookTests
    [Fact]
    public async Task DeleteBookById_ShouldReturnTrue()
    {

        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Book book = new(Guid.NewGuid())
        {
            Title = "Sample Book",
            Author = "Author Name",
        };
        Book addedBook = await repository.AddBook(book);

        Assert.True(await repository.DeleteBookById(addedBook.Id));
        Assert.Empty(await repository.GetAllBooks());
    }

    [Fact]
    public async Task DeleteBookById_ShouldReturnFalseWhenIdIsNull()
    {

        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Guid id = Guid.Empty;

        Assert.False(await repository.DeleteBookById(id));
    }
    #endregion
}
