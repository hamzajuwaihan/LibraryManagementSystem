using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;
/// <summary>
/// Delete Member Command Handler class for CQRS MediatR
/// </summary>
/// <param name="memberRepository"></param>
public class DeleteMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    /// <summary>
    /// Handle function implementation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken) => await _memberRepository.DeleteMemberById(request.Id);

}
