
using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.BookQueryHandlersTests;

public class GetAllBorrowedBooksQueryHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly GetAllBorrowedBooksQueryHandler _handler;

    public GetAllBorrowedBooksQueryHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new GetAllBorrowedBooksQueryHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenCalled_ReturnsListOfBorrowedBooks()
    {

        List<Book> borrowedBooks = new List<Book>
        {
            new Book(Guid.NewGuid()) { Title = "Borrowed Book 1", Author = "Author 1" },
            new Book(Guid.NewGuid()) { Title = "Borrowed Book 2", Author = "Author 2" }
        };

        _mockBookRepository.Setup(repo => repo.GetAllBorrowedBooks()).Returns(borrowedBooks.AsQueryable());

        GetAllBorrowedBooksQuery query = new GetAllBorrowedBooksQuery();


        List<Book> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

}
