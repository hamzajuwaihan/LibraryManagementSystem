using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class MemberRepository(DbAppContext context) : IMemberRepository
{
    private readonly DbAppContext _context = context;
    public async Task<Member> CreateMember(Member member)
    {
        bool emailExists = await _context.Members.AnyAsync(m => m.Email == member.Email);

        if (emailExists) throw new DuplicateEmailException();

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

    public async Task<List<Member>> GetAllMembers(int pageSize, int pageNumber)
    {
        int skip = (pageNumber - 1) * pageSize;

        return await _context.Members.Include(member => member.Books)
            .OrderBy(m => m.Name)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Member>> GetAllMembers()
    {

        return await _context.Members.ToListAsync();
    }

    public async Task<Member> GetMemberById(Guid id) => await _context.Members.FirstOrDefaultAsync(member => member.Id == id) ?? throw new MemberNotFoundException();

    public async Task<Member> UpdateMember(Guid memberId, string? newName, string? newEmail)
    {

        Member existingMember = await _context.Members.FindAsync(memberId) ?? throw new MemberNotFoundException();

        if (!string.IsNullOrEmpty(newEmail))
        {
            bool emailInUse = await _context.Members.AnyAsync(m => m.Email.Equals(newEmail, StringComparison.CurrentCultureIgnoreCase) && m.Id != memberId);

            if (emailInUse) throw new DuplicateEmailException();

            existingMember.Email = newEmail;
        }

        if (!string.IsNullOrEmpty(newName)) existingMember.Name = newName;


        await _context.SaveChangesAsync();

        return existingMember;
    }
}
