using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;
/// <summary>
/// Get all libraries in the DB handler class
/// </summary>
/// <param name="libraryRepository"></param>
public class GetAllLibrariesQueryHandler(ILibraryRepository libraryRepository) : IRequestHandler<GetAllLibrariesQuery, List<Library>>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;

    public async Task<List<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _libraryRepository.GetAllLibraries();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return [];
        }
    }
}
