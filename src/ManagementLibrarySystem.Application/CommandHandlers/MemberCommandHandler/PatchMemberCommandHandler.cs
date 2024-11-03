using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Common;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.CommandHandlers.MemberCommandHandler;

public class PatchMemberCommandHandler(IMemberRepository memberRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<PatchMemberCommand, Member>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Member> Handle(PatchMemberCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Email))
        {
            throw new InvalidPatchOperationException("At least one field (name or email) must be provided for an update.");
        }
        
        Guid id = Guid.Parse(_httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString()!);


        return await _memberRepository.UpdateMember(id, request.Name, request.Email);

    }
}
