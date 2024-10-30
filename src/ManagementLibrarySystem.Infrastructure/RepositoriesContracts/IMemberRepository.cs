using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IMemberRepository
{
    Task<Member?> GetMemberById(Guid id);
    Task<Member?> UpdateMemberById(Member member);
    Task<bool> DeleteMemberById(Guid id);
    Task<Member> CreateMember(Member member);
    IQueryable<Member> GetAllMembers();
}
