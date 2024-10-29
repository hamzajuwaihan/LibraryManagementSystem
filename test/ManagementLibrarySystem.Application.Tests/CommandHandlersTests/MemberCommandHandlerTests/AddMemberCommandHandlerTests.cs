using ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;
using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.MemberCommandHandlerTests;
public class AddMemberCommandHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly AddMemberCommandHandler _handler;

    public AddMemberCommandHandlerTests()
    {
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new AddMemberCommandHandler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task AddMember_ValidRequest_AddsMemberAndReturnsNewMember()
    {
        string memberName = "John Doe";
        string memberEmail = "john.doe@example.com";
        AddMemberCommand command = new(memberName, memberEmail);

        Member newMember = new Member(Guid.NewGuid())
        {
            Name = memberName,
            Email = memberEmail
        };

        _mockMemberRepository.Setup(repo => repo.AddMember(It.IsAny<Member>())).ReturnsAsync(newMember);

        Member result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(memberName, result.Name);
        Assert.Equal(memberEmail, result.Email);
        _mockMemberRepository.Verify(repo => repo.AddMember(It.IsAny<Member>()), Times.Once);
    }

    [Fact]
    public async Task AddMember_ExceptionThrown_ReturnsNull()
    {
        string memberName = "Jane Doe";
        string memberEmail = "jane.doe@example.com";
        AddMemberCommand command = new(memberName, memberEmail);

        _mockMemberRepository.Setup(repo => repo.AddMember(It.IsAny<Member>())).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        _mockMemberRepository.Verify(repo => repo.AddMember(It.IsAny<Member>()), Times.Once);
    }
}