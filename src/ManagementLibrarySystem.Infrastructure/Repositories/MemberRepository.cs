using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class MemberRepository(DbAppContext context) : IMemberRepository
{
    private readonly DbAppContext _context = context;
    public async Task<Member?> AddMember(Member member)
    {
        try
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return member;
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteMemberById(Guid id)
    {
        Member? member = await GetMemberById(id);

        if (member == null) return false;

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Member>> GetAllMembers()
    {
        List<Member> membersList = await _context.Members.ToListAsync();
        return membersList;
    }

    public async Task<Member?> GetMemberById(Guid id) => await _context.Members.FirstOrDefaultAsync(member => member.Id == id);

    public async Task<Member?> UpdateMemberById(Member member)
    {
        Member? existingMember = await _context.Members.FindAsync(member.Id);

        if (existingMember == null)
        {
            return null;
        }

        existingMember.Update(member.Name, member.Email);

        await _context.SaveChangesAsync();

        return existingMember;
    }
}
