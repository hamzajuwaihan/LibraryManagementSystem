using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.MemberQueries;

public class GetAllMembersQuery : IRequest<List<Member>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
