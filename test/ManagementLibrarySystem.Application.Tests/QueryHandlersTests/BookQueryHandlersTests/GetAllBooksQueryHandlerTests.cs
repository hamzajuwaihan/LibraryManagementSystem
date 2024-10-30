
using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.BookQueryHandlersTests;

public class GetAllBooksQueryHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly GetAllBooksQueryHandler _handler;

    public GetAllBooksQueryHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new GetAllBooksQueryHandler(_mockBookRepository.Object);
    }
    [Fact]
    public async Task Handle_WhenCalled_ReturnsListOfBooks()
    {
        // Create a list of books
        List<Book> books = new List<Book>
    {
        new(Guid.NewGuid()) { Title = "Book 1", Author = "Author 1" },
        new(Guid.NewGuid()) { Title = "Book 2", Author = "Author 2" }
    };

        // Setup the mock to return IQueryable<Book>
        _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(books.AsQueryable());

        GetAllBooksQuery query = new GetAllBooksQuery();

        List<Book> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

}
