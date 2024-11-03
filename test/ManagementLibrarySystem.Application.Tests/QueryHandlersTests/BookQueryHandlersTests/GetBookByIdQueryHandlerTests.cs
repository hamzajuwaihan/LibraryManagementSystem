
using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.BookQueryHandlersTests;

public class GetBookByIdQueryHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly GetBookByIdQueryHandler _handler;

    public GetBookByIdQueryHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new GetBookByIdQueryHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task GetBookByIdQueryHandler_WhenBookExists_ReturnsBook()
    {

        Guid bookId = Guid.NewGuid();
        Book expectedBook = new Book(bookId) { Title = "Sample Book", Author = "Author Name" };

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(expectedBook);

        GetBookByIdQuery query = new GetBookByIdQuery(bookId);


        Book? result = await _handler.Handle(query, CancellationToken.None);


        Assert.NotNull(result);
        Assert.Equal(expectedBook.Title, result.Title);
        Assert.Equal(expectedBook.Author, result.Author);
    }



    [Fact]
    public async Task GetBookByIdQueryHandler_WhenExceptionThrown_ThrowsException()
    {

        Guid bookId = Guid.NewGuid();

        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ThrowsAsync(new Exception("Database error"));

        GetBookByIdQuery query = new GetBookByIdQuery(bookId);

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
    }
}
