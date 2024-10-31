using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Application.QueryHandlers.MemberQueryHandlers;

public class GetAllMembersQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetAllMembersQuery, List<Member>>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    public async Task<List<Member>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken) => await _memberRepository.GetAllMembers(request.PageSize, request.PageNumber);
}
