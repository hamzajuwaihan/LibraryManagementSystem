using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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

    #region AddingMemberTests
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

        Member createdMember = await repository.AddMember(member);

        Assert.NotNull(createdMember);
        Assert.Equal("John Doe", createdMember.Name);
        Assert.Equal("john.doe@example.com", createdMember.Email);
        Assert.Single(context.Members);
    }

    [Fact]
    public async Task CreateMember_ShouldReturnExceptionWhenMemberIsNull()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Member newMember = null!;

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddMember(newMember));
    }
    #endregion

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

        Member createdMember = await repository.AddMember(member);
        Member? searchMember = await repository.GetMemberById(createdMember.Id);

        Assert.Equal(createdMember.Id, searchMember?.Id);
        Assert.Equal(createdMember.Name, searchMember?.Name);
        Assert.Equal(createdMember.Email, searchMember?.Email);
    }

    [Fact]
    public async Task GetMemberById_ShouldReturnNullWhenMemberNotFound()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Guid randomGuid = Guid.NewGuid();

        Member? searchMember = await repository.GetMemberById(randomGuid);

        Assert.Null(searchMember);
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
        Member addedMember = await repository.AddMember(member);

        Assert.True(await repository.DeleteMemberById(addedMember.Id));
        Assert.Empty(await repository.GetAllMembers());
    }

    [Fact]
    public async Task DeleteMemberById_ShouldReturnFalseWhenIdIsNull()
    {
        using DbAppContext context = CreateDbContext();
        MemberRepository repository = new(context);

        Guid id = Guid.Empty;

        Assert.False(await repository.DeleteMemberById(id));
    }
    #endregion
}