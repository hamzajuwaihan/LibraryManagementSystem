using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;
/// <summary>
/// Delete Library Command Class for MediatR (CQRS)
/// </summary>
/// <param name="libraryRepository"></param>
public class DeleteLibraryCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<DeleteLibraryCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    /// <summary>
    /// Handle function implemetnation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _libraryRepository.DeleteLibraryById(request.LibraryId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

    }
}
