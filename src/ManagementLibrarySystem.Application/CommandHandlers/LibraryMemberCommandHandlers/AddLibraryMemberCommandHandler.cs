using ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryMemberCommandHandlers;
/// <summary>
/// Add Library Member Command class for CQRS MediatR
/// </summary>
/// <param name="libraryRepository"></param>
public class AddLibraryMemberCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<AddLibraryMemberCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    /// <summary>
    /// Handler function implementation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(AddLibraryMemberCommand request, CancellationToken cancellationToken) =>  await _libraryRepository.AddMemberToLibrary(request.LibraryId, request.MemberId);

    
}
