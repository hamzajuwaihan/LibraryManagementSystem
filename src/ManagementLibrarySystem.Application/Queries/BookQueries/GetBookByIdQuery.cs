using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.BookQueries;

public class GetBookByIdQuery(Guid bookId) : IRequest<Book>
{
    public Guid BookId { get; } = bookId;
}
