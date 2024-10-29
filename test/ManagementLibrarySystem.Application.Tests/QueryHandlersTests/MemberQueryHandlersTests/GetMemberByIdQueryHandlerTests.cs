
using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Application.QueryHandlers.MemberQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.MemberQueryHandlersTests;

public class GetMemberByIdQueryHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly GetMemberByIdQueryHandler _handler;

    public GetMemberByIdQueryHandlerTests()
    {
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new GetMemberByIdQueryHandler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenMemberExists_ReturnsMember()
    {
        Guid memberId = Guid.NewGuid();
        Member expectedMember = new(memberId) { Name = "John Doe", Email = "hamza@gmail.com" };
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync(expectedMember);
        GetMemberByIdQuery query = new(memberId);

        Member? result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedMember.Name, result.Name);
    }

    [Fact]
    public async Task Handle_WhenMemberDoesNotExist_ReturnsNull()
    {
        Guid memberId = Guid.NewGuid();
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ReturnsAsync((Member?)null);
        GetMemberByIdQuery query = new(memberId);

        Member? result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_WhenExceptionThrown_ReturnsNull()
    {
        Guid memberId = Guid.NewGuid();
        _mockMemberRepository.Setup(repo => repo.GetMemberById(memberId)).ThrowsAsync(new Exception("Database error"));
        GetMemberByIdQuery query = new(memberId);

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
    }
}
