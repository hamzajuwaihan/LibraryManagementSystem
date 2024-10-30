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

        Book createdBook = await repository.CreateBook(book);

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

        Book book = null!;

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.CreateBook(book));

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


        Book createdBook = await repository.CreateBook(book);
        Book? searchBook = await repository.GetBookById(createdBook.Id);


        Assert.Equal(createdBook.Id, searchBook?.Id);
        Assert.Equal(createdBook.Title, searchBook?.Title);
        Assert.Equal(createdBook.Author, searchBook?.Author);
    }

    [Fact]
    public async Task GetAllBooks_ShouldReturnAllBooks()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Book book1 = new Book(Guid.NewGuid())
        {
            Title = "Book One",
            Author = "Author One"
        };

        Book book2 = new Book(Guid.NewGuid())
        {
            Title = "Book Two",
            Author = "Author Two"
        };

        await repository.CreateBook(book1);
        await repository.CreateBook(book2);

        List<Book> books = await repository.GetAllBooks().ToListAsync();
        Assert.NotNull(books);
        Assert.Equal(2, books.Count());
        Assert.Contains(books, b => b.Title == "Book One" && b.Author == "Author One");
        Assert.Contains(books, b => b.Title == "Book Two" && b.Author == "Author Two");
    }

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
        Book addedBook = await repository.CreateBook(book);

        Assert.True(await repository.DeleteBookById(addedBook.Id));
        Assert.Empty(await repository.GetAllBooks().ToListAsync());
    }

    #endregion

    #region UpdateBookTests
    [Fact]
    public async Task UpdateBookById_ShouldUpdateBookDetailsSuccessfully()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);

        Book book = new(Guid.NewGuid())
        {
            Title = "Original Title",
            Author = "Original Author"
        };

        await repository.CreateBook(book);


        book.Update("Updated Title", "Updated Author", book.IsBorrowed, book.BorrowedDate, book.BorrowedBy);
        Book? updatedBook = await repository.UpdateBookById(book.Id, book);


        Assert.NotNull(updatedBook);
        Assert.Equal("Updated Title", updatedBook?.Title);
        Assert.Equal("Updated Author", updatedBook?.Author);
    }

    [Fact]
    public async Task PatchBookById_ShouldUpdateOnlyProvidedFields()
    {
        using DbAppContext context = CreateDbContext();
        BookRepository repository = new(context);


        Book book = new(Guid.NewGuid())
        {
            Title = "Original Title",
            Author = "Original Author",
            IsBorrowed = false,
            BorrowedDate = null,
            BorrowedBy = null
        };

        await repository.CreateBook(book);

        Book patchBook = new(book.Id)
        {
            Title = "Patched Title",
            Author = book.Author
        };
        Book? patchedBook = await repository.PatchBookById(book.Id, patchBook);


        Assert.NotNull(patchedBook);
        Assert.Equal("Patched Title", patchedBook?.Title);
        Assert.Equal("Original Author", patchedBook?.Author);
        Assert.Equal(book.IsBorrowed, patchedBook?.IsBorrowed);
    }

    #endregion

}
