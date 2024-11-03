using ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryMemberCommandHandlers;

public class RemoveLibraryMemberCommandHandler
{
    private readonly ILibraryRepository _libraryRepository;

    public RemoveLibraryMemberCommandHandler(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    public async Task<bool> Handle(RemoveLibraryMemberCommand request, CancellationToken cancellationToken)
    {
        return await _libraryRepository.RemoveMemberFromLibrary(request.LibraryId, request.MemberId);
    }
}
