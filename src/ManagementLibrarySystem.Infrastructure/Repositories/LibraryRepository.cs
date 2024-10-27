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

        if (library == null) return false;

        _context.Libraries.Remove(library);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Library>> GetAllLibraries()
    {
        List<Library> libraryList = await _context.Libraries.ToListAsync();
        return libraryList;
    }

    public async Task<Library?> GetLibraryById(Guid id) => await _context.Libraries.FirstOrDefaultAsync(library => library.Id == id);


    public async Task<Library?> UpdateLibraryById(Library library)
    {
        var existingLibrary = await _context.Libraries.FindAsync(library.Id);

        if (existingLibrary == null)
        {
            return null;
        }

        existingLibrary.Update(library.Name);

        await _context.SaveChangesAsync();

        return existingLibrary;
    }
}
