using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class PatchBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly PatchBookByIdCommandHandler _handler;

    public PatchBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _handler = new PatchBookByIdCommandHandler(_mockBookRepository.Object);
    }

    [Fact]
    public async Task PatchBook_BookExists_PatchesBookSuccessfully()
    {
        Guid bookId = Guid.NewGuid();
        Book existingBook = new(bookId) { Title = "Old Title", Author = "Old Author", IsBorrowed = false };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(existingBook);
        PatchBookByIdCommand command = new(bookId, "New Title", null, null, null, null);

        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("New Title", result.Title);
        Assert.Equal("Old Author", result.Author);
        Assert.False(result.IsBorrowed);
        _mockBookRepository.Verify(repo => repo.PatchBookById(bookId, It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task PatchBook_BookDoesNotExist_ThrowsKeyNotFoundException()
    {
        Guid bookId = Guid.NewGuid();
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync((Book?)null);
        PatchBookByIdCommand command = new(bookId, "New Title", null, null, null, null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task PatchBook_BookExists_OnlyUpdatesProvidedFields()
    {
        Guid bookId = Guid.NewGuid();
        Book existingBook = new(bookId) { Title = "Old Title", Author = "Old Author", IsBorrowed = false };
        _mockBookRepository.Setup(repo => repo.GetBookById(bookId)).ReturnsAsync(existingBook);
        PatchBookByIdCommand command = new(bookId, null, "New Author", null, null, null);

        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Old Title", result.Title);
        Assert.Equal("New Author", result.Author);
        Assert.False(result.IsBorrowed);
        _mockBookRepository.Verify(repo => repo.PatchBookById(bookId, It.IsAny<Book>()), Times.Once);
    }
}
