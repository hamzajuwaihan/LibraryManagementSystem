using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class UpdateBookByIdCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly UpdateBookByIdCommandHandler _handler;

    public UpdateBookByIdCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new UpdateBookByIdCommandHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task UpdateBookById_BookExists_UpdatesAndReturnsBook()
    {
        Guid bookId = Guid.NewGuid();
        Book existingBook = new(bookId) { Title = "Old Title", Author = "Old Author", IsBorrowed = false };
        
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(existingBook);

        UpdateBookByIdCommand command = new(bookId, "New Title", "New Author", true, DateTime.UtcNow, Guid.NewGuid());

        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("New Title", result.Title);
        Assert.Equal("New Author", result.Author);
        Assert.True(result.IsBorrowed);
        _mockBookRepository.Verify(repo => repo.UpdateBookById(bookId, It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task UpdateBookById_BookNotFound_ThrowsKeyNotFoundException()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);

        UpdateBookByIdCommand command = new(bookId, "New Title", "New Author", true, DateTime.UtcNow, Guid.NewGuid());
        
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
