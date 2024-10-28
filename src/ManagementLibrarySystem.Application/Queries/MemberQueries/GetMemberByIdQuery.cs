using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.MemberQueries;

public class GetMemberByIdQuery(Guid memberId) : IRequest<Member>
{
    public Guid MemberId { get; } = memberId;
}
