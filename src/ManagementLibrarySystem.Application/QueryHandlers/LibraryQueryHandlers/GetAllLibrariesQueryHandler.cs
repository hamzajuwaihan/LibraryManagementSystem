using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;
/// <summary>
/// Get all libraries in the DB handler class
/// </summary>
/// <param name="libraryRepository"></param>
public class GetAllLibrariesQueryHandler(ILibraryRepository libraryRepository) : IRequestHandler<GetAllLibrariesQuery, List<Library>>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;

    public async Task<List<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken) => await _libraryRepository.GetAllLibraries(request.PageSize, request.PageNumber);

}
