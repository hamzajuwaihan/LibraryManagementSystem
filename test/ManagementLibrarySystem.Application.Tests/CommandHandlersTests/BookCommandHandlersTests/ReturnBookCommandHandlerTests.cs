using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
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
    public async Task ReturnBook_BookNotFound_ReturnsBookNotFoundException()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);

        ReturnBookCommand command = new(bookId);

        await Assert.ThrowsAsync<BookNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ReturnBook_BookNotCurrentlyBorrowed_ReturnsBookIsNotCurrentlyBorrowedException()
    {
        Guid bookId = Guid.NewGuid();
        Book notBorrowedBook = new(bookId) { Title = "Test Book", Author = "Author", IsBorrowed = false };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(notBorrowedBook);

        ReturnBookCommand command = new(bookId);


        await Assert.ThrowsAsync<BookIsNotCurrentlyBorrowedException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
