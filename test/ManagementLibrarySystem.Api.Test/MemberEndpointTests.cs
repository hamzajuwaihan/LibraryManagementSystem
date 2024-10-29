using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Api.Test;

public class MemberEndpointTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly HttpClient _client;

    public MemberEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();

        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _mediatorMock.Object);
                });
            });

        _client = webAppFactory.CreateClient();
    }
    [Fact]
    public async Task Post_Member_ReturnsCreated()
    {
        AddMemberCommand newMember = new("Hamza", "Hamza@gmail.com");

        Member createdMember = new Member(Guid.NewGuid()) { Name = "Jane Doe", Email = "Hamza@gmail.com" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdMember);

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/member", newMember);

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        Member? returnedMember = await response.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(returnedMember);
        Assert.Equal(createdMember.Id, returnedMember.Id);
    }

    [Fact]
    public async Task Delete_Member_ReturnsNoContent()
    {
        Guid memberId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMemberCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        HttpResponseMessage response = await _client.DeleteAsync($"/api/member/{memberId}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Get_MemberById_ReturnsOk()
    {
        Guid memberId = Guid.NewGuid();
        Member member = new Member(memberId) { Name = "Existing Member", Email = "Hamza@gmail.com" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetMemberByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(member);

        HttpResponseMessage response = await _client.GetAsync($"/api/member/{memberId}");

        response.EnsureSuccessStatusCode();
        Member? returnedMember = await response.Content.ReadFromJsonAsync<Member>();
        Assert.NotNull(returnedMember);
        Assert.Equal(memberId, returnedMember.Id);
        Assert.Equal(member.Name, returnedMember.Name);
    }

    [Fact]
    public async Task Get_AllMembers_ReturnsOk()
    {
        List<Member> members = new List<Member>
            {
                new Member(Guid.NewGuid()){  Name = "Member 1" , Email = "Email!@gmailc.om" },
                new Member(Guid.NewGuid()){  Name = "Member 2", Email= "Email2@gmail.com" }
            };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllMembersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(members);

        HttpResponseMessage response = await _client.GetAsync("/api/member");

        response.EnsureSuccessStatusCode();
        List<Member>? returnedMembers = await response.Content.ReadFromJsonAsync<List<Member>>();
        Assert.NotNull(returnedMembers);
        Assert.Equal(2, returnedMembers.Count);
    }

}
