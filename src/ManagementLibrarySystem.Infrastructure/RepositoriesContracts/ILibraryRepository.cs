using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface ILibraryRepository
{
    Task<Library?> GetLibraryById(Guid id);

    Task<Library?> UpdateLibraryById(Library library);

    Task<bool> DeleteLibraryById(Guid id);

    Task<List<Library>> GetAllLibraries();

    Task<Library> AddLibrary(Library library);
}
