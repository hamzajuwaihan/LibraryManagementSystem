using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Library;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class LibraryRepository(DbAppContext context) : ILibraryRepository
{
    private readonly DbAppContext _context = context;

    public async Task<Library> CreateLibrary(Library library)
    {
        bool nameExists = await _context.Libraries.AnyAsync(l => l.Name == library.Name);

        if (nameExists) throw new DuplicateLibraryNameException();

        await _context.Libraries.AddAsync(library);

        await _context.SaveChangesAsync();

        return library;
    }

    public async Task<bool> DeleteLibraryById(Guid id)
    {
        Library? library = await GetLibraryById(id) ?? throw new LibraryNotFoundException();

        _context.Libraries.Remove(library);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Library>> GetAllLibraries(int pageSize, int pageNumber)
    {
        int skip = (pageNumber - 1) * pageSize;

        return await _context.Libraries
            .OrderBy(l => l.Name)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Library>> GetAllLibraries() => await _context.Libraries.ToListAsync();


    public async Task<Library> GetLibraryById(Guid id) => await _context.Libraries.Include(library => library.Books).FirstOrDefaultAsync(library => library.Id == id) ?? throw new LibraryNotFoundException();


    public async Task<Library?> UpdateLibrary(Library library)
    {
        Library? existingLibrary = await _context.Libraries.FindAsync(library.Id) ?? throw new LibraryNotFoundException();

        existingLibrary.Update(library.Name);

        await _context.SaveChangesAsync();

        return existingLibrary;
    }

    public async Task<bool> AddMemberToLibrary(Guid libraryId, Guid memberId)
    {
        Library? library = await _context.Libraries
            .Include(l => l.Members)
            .FirstOrDefaultAsync(l => l.Id == libraryId) ?? throw new LibraryNotFoundException();

        Member? member = await _context.Members.FindAsync(memberId) ?? throw new MemberNotFoundException();

        if (library.Members.Any(m => m.Id == memberId)) throw new LibraryAlreadyHasThisMember();

        library.Members.Add(member);

        await _context.SaveChangesAsync();

        return true;
    }
}
