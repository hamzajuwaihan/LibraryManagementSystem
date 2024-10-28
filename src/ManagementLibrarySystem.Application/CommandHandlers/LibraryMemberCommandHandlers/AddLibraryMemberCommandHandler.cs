using ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryMemberCommandHandlers;

public class AddLibraryMemberCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<AddLibraryMemberCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    
   public async Task<bool> Handle(AddLibraryMemberCommand request, CancellationToken cancellationToken)
    {
        bool result = await _libraryRepository.AddMemberToLibrary(request.LibraryId, request.MemberId);

        return result;
    }
}
