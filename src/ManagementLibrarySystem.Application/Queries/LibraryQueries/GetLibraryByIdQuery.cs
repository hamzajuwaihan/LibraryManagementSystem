using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.LibraryQueries;

public class GetLibraryByIdQuery(Guid id) : IRequest<Library>
{
    public Guid Id { get; } = id;
}
