
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
        List<Book> books = new List<Book>
    {
        new(Guid.NewGuid()) { Title = "Book 1", Author = "Author 1" },
        new(Guid.NewGuid()) { Title = "Book 2", Author = "Author 2" }
    };
        GetAllBooksQuery query = new GetAllBooksQuery();

        _mockBookRepository.Setup(repo => repo.GetAllBooks(query.PageSize, query.PageNumber)).ReturnsAsync(books);


        List<Book> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

}
