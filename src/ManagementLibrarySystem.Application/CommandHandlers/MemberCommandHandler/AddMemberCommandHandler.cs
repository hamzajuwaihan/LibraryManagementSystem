using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;

public class AddMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<AddMemberCommand, Member>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    public async Task<Member> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Member newMember = new(Guid.NewGuid())
            {
                Name = request.Name,
                Email = request.Email,
            };

            return await _memberRepository.AddMember(newMember);
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            throw;
        }
    }
}
