using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class ReturnBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly ReturnBookCommandHandler _handler;

    public ReturnBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new ReturnBookCommandHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task ReturnBook_BookExists_ReturnsSuccessfully()
    {
        Guid bookId = Guid.NewGuid();
        Book borrowedBook = new(bookId) { Title = "Test Book", Author = "Author", IsBorrowed = true };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(borrowedBook);

        ReturnBookCommand command = new(bookId);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Book returned successfully.", result);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(bookId, It.Is<Book>(b => !b.IsBorrowed)), Times.Once);
    }

    [Fact]
    public async Task ReturnBook_BookNotFound_ReturnsNotFoundMessage()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);

        ReturnBookCommand command = new(bookId);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Book not found.", result);
    }

    [Fact]
    public async Task ReturnBook_BookNotCurrentlyBorrowed_ReturnsNotBorrowedMessage()
    {
        Guid bookId = Guid.NewGuid();
        Book notBorrowedBook = new(bookId) { Title = "Test Book", Author = "Author", IsBorrowed = false };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(notBorrowedBook);

        ReturnBookCommand command = new(bookId);

        string result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Book is not currently borrowed.", result);
    }
}
