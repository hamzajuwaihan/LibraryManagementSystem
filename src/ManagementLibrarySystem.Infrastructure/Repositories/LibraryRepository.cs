using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
namespace ManagementLibrarySystem.Infrastructure.Repositories;

public class LibraryRepository(DbAppContext context) : ILibraryRepository
{
    private readonly DbAppContext _context = context;

    public async Task<Library> AddLibrary(Library library)
    {
        try
        {
            await _context.Libraries.AddAsync(library);
            await _context.SaveChangesAsync();
            return library;
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteLibraryById(Guid id)
    {
        Library? library = await GetLibraryById(id);

        if (library is null) return false;

        _context.Libraries.Remove(library);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Library>> GetAllLibraries() => await _context.Libraries.ToListAsync();
    
    public async Task<Library?> GetLibraryById(Guid id) => await _context.Libraries.Include(library => library.Books).FirstOrDefaultAsync(library => library.Id == id);


    public async Task<Library?> UpdateLibraryById(Library library)
    {
        Library? existingLibrary = await _context.Libraries.FindAsync(library.Id);

        if (existingLibrary == null) return null;
        
        existingLibrary.Update(library.Name);

        await _context.SaveChangesAsync();

        return existingLibrary;
    }

    public async Task<bool> AddMemberToLibrary(Guid libraryId, Guid memberId)
    {

        Library? library = await _context.Libraries
            .Include(l => l.Members)
            .FirstOrDefaultAsync(l => l.Id == libraryId);


        Member? member = await _context.Members.FindAsync(memberId);

        if (library == null || member == null) return false;

        if (library.Members.Any(m => m.Id == memberId)) return false;

        library.Members.Add(member);

        await _context.SaveChangesAsync();

        return true;
    }
}
