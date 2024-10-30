using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;
/// <summary>
/// Add member command handler class for CQRS MediatR
/// </summary>
/// <param name="memberRepository"></param>
public class AddMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<AddMemberCommand, Member>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    /// <summary>
    /// Handle fucntion implemetnation
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Member> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {

        Member newMember = new(Guid.NewGuid())
        {
            Name = request.Name,
            Email = request.Email,
        };

        return await _memberRepository.CreateMember(newMember);

    }
}
