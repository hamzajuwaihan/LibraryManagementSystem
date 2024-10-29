
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class AddBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly AddBookCommandHandler _handler;

    public AddBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new AddBookCommandHandler(_mockBookRepository.Object, _mockLibraryRepository.Object);
    }

    [Fact]
    public async Task AddBook_ShouldAddBook_WhenLibraryExists()
    {

        Guid libraryId = Guid.NewGuid();
        AddBookCommand addBookCommand = new("Test Title", "Test Author", libraryId);
        Library library = new(libraryId)
        {
            Name = "Test Library"
        };

        _mockLibraryRepository
            .Setup(repo => repo.GetLibraryById(libraryId))
            .ReturnsAsync(library);

        _mockBookRepository
            .Setup(repo => repo.AddBook(It.IsAny<Book>()))
            .ReturnsAsync((Book book) => book);

        Book result = await _handler.Handle(addBookCommand, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(addBookCommand.Title, result.Title);
        Assert.Equal(addBookCommand.Author, result.Author);
        Assert.Equal(addBookCommand.LibraryId, result.LibraryId);
        _mockBookRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task AddBook_ShouldThrowException_WhenLibraryDoesNotExist()
    {

        Guid libraryId = Guid.NewGuid();
        AddBookCommand addBookCommand = new("Test Title", "Test Author", libraryId);

        _mockLibraryRepository
            .Setup(repo => repo.GetLibraryById(libraryId))
            .ReturnsAsync((Library?)null);


        Exception exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(addBookCommand, CancellationToken.None));

        Assert.Equal($"Library with ID {libraryId} does not exist.", exception.Message);
        _mockBookRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task AddBook_ShouldThrowException_WhenBookRepositoryFails()
    {

        Guid libraryId = Guid.NewGuid();
        AddBookCommand addBookCommand = new("Test Title", "Test Author", libraryId);
        Library library = new(libraryId)
        {
            Name = "Test Library"
        };

        _mockLibraryRepository
            .Setup(repo => repo.GetLibraryById(libraryId))
            .ReturnsAsync(library);

        _mockBookRepository
            .Setup(repo => repo.AddBook(It.IsAny<Book>()))
            .ThrowsAsync(new Exception("Database error"));


        Exception exception = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(addBookCommand, CancellationToken.None));

        Assert.Equal("Database error", exception.Message);
        _mockBookRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.Once);
    }

}
