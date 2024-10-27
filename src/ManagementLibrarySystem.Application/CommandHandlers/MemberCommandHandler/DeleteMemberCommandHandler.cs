using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;

public class DeleteMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _memberRepository.DeleteMemberById(request.MemberId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}
