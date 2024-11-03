using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface IMemberRepository
{
    Task<Member> GetMemberById(Guid id);
    Task<Member> UpdateMember(Guid id, string? name, string? email);
    Task<bool> DeleteMemberById(Guid id);
    Task<Member> CreateMember(Member member);
    Task<List<Member>> GetAllMembers(int pageSize, int pageNumber);
    Task<List<Member>> GetAllMembers();
}
