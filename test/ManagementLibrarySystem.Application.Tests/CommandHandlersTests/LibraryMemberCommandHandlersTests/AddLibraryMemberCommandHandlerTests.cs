

using ManagementLibrarySystem.Application.CommandHandlers.LibraryMemberCommandHandlers;
using ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.LibraryMemberCommandHandlersTests;

public class AddLibraryMemberCommandHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly AddLibraryMemberCommandHandler _handler;

    public AddLibraryMemberCommandHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new AddLibraryMemberCommandHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task AddLibrary_ValidRequest_AddsMemberToLibraryAndReturnsTrue()
    {
        Guid libraryId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        AddLibraryMemberCommand command = new(libraryId, memberId);

        _mockLibraryRepository.Setup(repo => repo.AddMemberToLibrary(libraryId, memberId)).ReturnsAsync(true);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        _mockLibraryRepository.Verify(repo => repo.AddMemberToLibrary(libraryId, memberId), Times.Once);
    }

    [Fact]
    public async Task AddLibrary_InvalidRequest_ReturnsFalse()
    {
        Guid libraryId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();
        AddLibraryMemberCommand command = new(libraryId, memberId);

        _mockLibraryRepository.Setup(repo => repo.AddMemberToLibrary(libraryId, memberId)).ReturnsAsync(false);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _mockLibraryRepository.Verify(repo => repo.AddMemberToLibrary(libraryId, memberId), Times.Once);
    }


}
