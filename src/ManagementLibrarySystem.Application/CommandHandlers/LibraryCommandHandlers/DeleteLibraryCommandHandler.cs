using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;

public class DeleteLibraryCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<DeleteLibraryCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
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
