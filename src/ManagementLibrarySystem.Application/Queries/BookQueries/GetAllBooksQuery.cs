using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.BookQueries;

public class GetAllBooksQuery : IRequest<List<Book>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
