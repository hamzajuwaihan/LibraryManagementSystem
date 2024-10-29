
using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Application.QueryHandlers.MemberQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.MemberQueryHandlersTests;

public class GetAllMembersQueryHandlerTests
{
    private readonly Mock<IMemberRepository> _mockMemberRepository;
    private readonly GetAllMembersQueryHandler _handler;

    public GetAllMembersQueryHandlerTests()
    {
        _mockMemberRepository = new Mock<IMemberRepository>();
        _handler = new GetAllMembersQueryHandler(_mockMemberRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenMembersExist_ReturnsListOfMembers()
    {
        var expectedMembers = new List<Member>
        {
            new(Guid.NewGuid()) { Name = "Member 1", Email= "Hamza@gmail.com" },
            new(Guid.NewGuid()) { Name = "Member 2" , Email= "Hamza@gmail.com" }
        };

        _mockMemberRepository.Setup(repo => repo.GetAllMembers()).ReturnsAsync(expectedMembers);

        var query = new GetAllMembersQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedMembers.Count, result.Count);
    }

    [Fact]
    public async Task Handle_WhenNoMembersExist_ReturnsEmptyList()
    {
        _mockMemberRepository.Setup(repo => repo.GetAllMembers()).ReturnsAsync(new List<Member>());

        var query = new GetAllMembersQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result); 
    }

    [Fact]
    public async Task Handle_WhenExceptionThrown_ReturnsNull()
    {
        _mockMemberRepository.Setup(repo => repo.GetAllMembers()).ThrowsAsync(new Exception("Database error"));

        var query = new GetAllMembersQuery();

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
    }
}
