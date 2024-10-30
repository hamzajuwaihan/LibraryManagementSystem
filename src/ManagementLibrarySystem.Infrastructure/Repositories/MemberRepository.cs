using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class MemberRepository(DbAppContext context) : IMemberRepository
{
    private readonly DbAppContext _context = context;
    public async Task<Member> CreateMember(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;

    }

    public async Task<bool> DeleteMemberById(Guid id)
    {
        Member? member = await GetMemberById(id) ?? throw new MemberNotFoundException();

        _context.Members.Remove(member);
        
        await _context.SaveChangesAsync();
        return true;
    }

    public IQueryable<Member> GetAllMembers() =>  _context.Members;

    public async Task<Member?> GetMemberById(Guid id) => await _context.Members.FirstOrDefaultAsync(member => member.Id == id);

    public async Task<Member?> UpdateMemberById(Member member)
    {
        Member? existingMember = await _context.Members.FindAsync(member.Id) ?? throw new MemberNotFoundException();

        existingMember.Update(member.Name, member.Email);

        await _context.SaveChangesAsync();

        return existingMember;
    }
}
