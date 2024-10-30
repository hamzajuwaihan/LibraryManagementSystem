
using ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;
using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.LibraryCommandHandlersTests;

public class DeleteLibraryCommandHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly DeleteLibraryCommandHandler _handler;

    public DeleteLibraryCommandHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new DeleteLibraryCommandHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task DeleteLibrary_ValidRequest_DeletesLibraryAndReturnsTrue()
    {
        Guid libraryId = Guid.NewGuid();
        DeleteLibraryCommand command = new(libraryId);

        _mockLibraryRepository.Setup(repo => repo.DeleteLibraryById(libraryId)).ReturnsAsync(true);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);

    }

    [Fact]
    public async Task DeleteLibrary_LibraryNotFound_ReturnsFalse()
    {
        Guid libraryId = Guid.NewGuid();
        DeleteLibraryCommand command = new(libraryId);

        _mockLibraryRepository.Setup(repo => repo.DeleteLibraryById(libraryId)).ReturnsAsync(false);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
    }

}
