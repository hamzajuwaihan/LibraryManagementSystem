using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.MemberQueryHandlers;

public class GetMemberByIdQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetMemberByIdQuery, Member?>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    public async Task<Member?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken) => await _memberRepository.GetMemberById(request.MemberId);

}
