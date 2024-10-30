using ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;
using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.MemberCommandHandlerTests;

public class DeleteMemberCommandHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly DeleteMemberCommandHandler _handler;

    public DeleteMemberCommandHandlerTests()
    {
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new DeleteMemberCommandHandler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task DeleteMember_ValidRequest_DeletesMemberAndReturnsTrue()
    {
        Guid memberId = Guid.NewGuid();
        DeleteMemberCommand command = new(memberId);

        _mockMemberRepository.Setup(repo => repo.DeleteMemberById(memberId)).ReturnsAsync(true);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        _mockMemberRepository.Verify(repo => repo.DeleteMemberById(memberId), Times.Once);
    }

    [Fact]
    public async Task DeleteMember_InvalidRequest_ReturnsFalse()
    {
        Guid memberId = Guid.NewGuid();
        DeleteMemberCommand command = new(memberId);

        _mockMemberRepository.Setup(repo => repo.DeleteMemberById(memberId)).ReturnsAsync(false);

        bool result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _mockMemberRepository.Verify(repo => repo.DeleteMemberById(memberId), Times.Once);
    }


}