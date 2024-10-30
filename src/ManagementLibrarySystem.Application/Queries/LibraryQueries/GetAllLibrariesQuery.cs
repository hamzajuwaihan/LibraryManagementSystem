using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.LibraryQueries;

public class GetAllLibrariesQuery : IRequest<List<Library>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
