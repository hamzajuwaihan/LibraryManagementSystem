using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new DeleteBookCommandHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task Handle_BookExists_DeletesBookSuccessfully()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.DeleteBookById(bookId)).ReturnsAsync(true);

        DeleteBookCommand command = new(bookId);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        _mockBookRepository.Verify(repo => repo.DeleteBookById(bookId), Times.Once);
    }

    [Fact]
    public async Task Handle_BookDoesNotExist_ReturnsFalse()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.DeleteBookById(bookId)).ReturnsAsync(false);

        DeleteBookCommand command = new(bookId);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _mockBookRepository.Verify(repo => repo.DeleteBookById(bookId), Times.Once);
    }
}
