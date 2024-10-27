using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IMemberRepository
{
    Task<Member?> GetMemberById(Guid id);

    Task<Member?> UpdateMemberById(Member member);

    Task<bool> DeleteMemberById(Guid id);

    Task<Member?> AddMember(Member member);

    Task<List<Member>> GetAllMembers();
}
