using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.LibraryQueries;

public class GetAllLibrariesQuery : IRequest<List<Library>>
{

}
