using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;

public class GetLibraryByIdQueryHandler(ILibraryRepository libraryRepository) : IRequestHandler<GetLibraryByIdQuery, Library?>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    public async Task<Library?> Handle(GetLibraryByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Library? result = await _libraryRepository.GetLibraryById(request.LibraryId);
            return result;
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            throw;
        }
    }
}
