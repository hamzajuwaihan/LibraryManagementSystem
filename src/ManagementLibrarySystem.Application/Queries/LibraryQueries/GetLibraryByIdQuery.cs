using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.LibraryQueries;

public class GetLibraryByIdQuery(Guid libraryId) : IRequest<Library>
{
    public Guid LibraryId { get; } = libraryId;
}
