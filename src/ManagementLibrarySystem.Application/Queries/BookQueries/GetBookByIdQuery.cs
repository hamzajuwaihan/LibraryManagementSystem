using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.BookQueries;

public class GetBookByIdQuery(Guid id) : IRequest<Book>
{
    public Guid Id { get; } = id;
}
