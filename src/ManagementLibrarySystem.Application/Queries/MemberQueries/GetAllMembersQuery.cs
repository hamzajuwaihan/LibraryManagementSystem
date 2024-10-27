using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.MemberQueries;

public class GetAllMembersQuery : IRequest<List<Member>> { }
