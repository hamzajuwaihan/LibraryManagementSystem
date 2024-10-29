
using ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;
using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.LibraryCommandHandlersTests;

public class AddLibraryCommandHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly AddLibraryCommandHandler _handler;

    public AddLibraryCommandHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new AddLibraryCommandHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task AddLibrary_ValidRequest_AddsLibraryAndReturnsLibrary()
    {
        string libraryName = "New Library";
        AddLibraryCommand command = new(libraryName);
        Library newLibrary = new(Guid.NewGuid())
        {
            Name = libraryName
        };

        _mockLibraryRepository.Setup(repo => repo.AddLibrary(It.IsAny<Library>())).ReturnsAsync(newLibrary);

        Library result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(libraryName, result.Name);
        _mockLibraryRepository.Verify(repo => repo.AddLibrary(It.Is<Library>(lib => lib.Name == libraryName)), Times.Once);
    }

    [Fact]
    public async Task AddLibrary_ExceptionThrown_LogsErrorAndThrows()
    {
        string libraryName = "New Library";
        AddLibraryCommand command = new(libraryName);

        _mockLibraryRepository.Setup(repo => repo.AddLibrary(It.IsAny<Library>())).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockLibraryRepository.Verify(repo => repo.AddLibrary(It.IsAny<Library>()), Times.Once);
    }
}
