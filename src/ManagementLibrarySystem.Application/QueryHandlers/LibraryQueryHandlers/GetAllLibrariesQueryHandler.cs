using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;

public class GetAllLibrariesQueryHandler(ILibraryRepository libraryRepository) : IRequestHandler<GetAllLibrariesQuery, List<Library>>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;

    public async Task<List<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken) => await _libraryRepository.GetAllLibraries();
}
