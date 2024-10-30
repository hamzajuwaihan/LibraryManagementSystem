using ManagementLibrarySystem.Domain.Entities;

namespace ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

public interface ILibraryRepository
{
    Task<Library?> GetLibraryById(Guid id);
    Task<Library?> UpdateLibrary(Library library);
    Task<bool> DeleteLibraryById(Guid id);
    IQueryable<Library> GetAllLibraries();
    Task<Library> CreateLibrary(Library library);
    Task<bool> AddMemberToLibrary(Guid library, Guid member);
}
