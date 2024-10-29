using System;
using ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;
using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.LibraryCommandHandlersTests;

public class AddBookToLibraryCommandHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly AddBookToLibraryCommandHandler _handler;

    public AddBookToLibraryCommandHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new AddBookToLibraryCommandHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task AddBookToLibrary_LibraryExists_AddsBookToLibraryAndReturnsTrue()
    {
        Guid libraryId = Guid.NewGuid();
        Guid bookId = Guid.NewGuid();
        Library library = new(libraryId)
        {
            Name = "Test Lib"
        };

        _mockLibraryRepository.Setup(repo => repo.GetLibraryById(libraryId)).ReturnsAsync(library);

        AddBookToLibraryCommand command = new AddBookToLibraryCommand(libraryId, bookId);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        Assert.Contains(library.Books, b => b.Id == bookId);
        _mockLibraryRepository.Verify(repo => repo.UpdateLibraryById(library), Times.Once);
    }

    [Fact]
    public async Task AddBookToLibrary_LibraryDoesNotExist_ReturnsFalse()
    {
        Guid libraryId = Guid.NewGuid();
        Guid bookId = Guid.NewGuid();

        _mockLibraryRepository.Setup(repo => repo.GetLibraryById(libraryId)).ReturnsAsync((Library?)null);

        AddBookToLibraryCommand command = new AddBookToLibraryCommand(libraryId, bookId);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _mockLibraryRepository.Verify(repo => repo.UpdateLibraryById(It.IsAny<Library>()), Times.Never);
    }
}
