using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.MemberQueryHandlers;

public class GetAllMembersQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetAllMembersQuery, List<Member>>
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    public async Task<List<Member>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Member> result = await _memberRepository.GetAllMembers();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
