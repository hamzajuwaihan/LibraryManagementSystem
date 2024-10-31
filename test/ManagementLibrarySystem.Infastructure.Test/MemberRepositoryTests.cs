using ManagementLibrarySystem.Infrastructure.DB;

namespace ManagementLibrarySystem.Infastructure.Test;


public class MemberRepositoryTests
{
    private DbAppContext CreateDbContext()
    {
        DbContextOptions<DbAppContext> options = new DbContextOptionsBuilder<DbAppContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DbAppContext(options);
    }


    [Fact]
    public async Task CreateMember_ShouldAddMemberToDatabase()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Member member = new(Guid.NewGuid())
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
        };

        Member createdMember = await repository.CreateMember(member);

        Assert.NotNull(createdMember);
        Assert.Equal("John Doe", createdMember.Name);
        Assert.Equal("john.doe@example.com", createdMember.Email);
        Assert.Single(context.Members);
    }


    #region GetMemberTests
    [Fact]
    public async Task GetMemberById_ShouldReturnMemberSuccessfully()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Guid memberId = Guid.NewGuid();

        Member member = new(memberId)
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
        };

        Member createdMember = await repository.CreateMember(member);
        Member? searchMember = await repository.GetMemberById(createdMember.Id);

        Assert.Equal(createdMember.Id, searchMember?.Id);
        Assert.Equal(createdMember.Name, searchMember?.Name);
        Assert.Equal(createdMember.Email, searchMember?.Email);
    }


    #endregion

    #region DeleteMemberTests
    [Fact]
    public async Task DeleteMemberById_ShouldReturnTrue()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Member member = new(Guid.NewGuid())
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
        };
        Member addedMember = await repository.CreateMember(member);

        Assert.True(await repository.DeleteMemberById(addedMember.Id));
    }

    #endregion

}
