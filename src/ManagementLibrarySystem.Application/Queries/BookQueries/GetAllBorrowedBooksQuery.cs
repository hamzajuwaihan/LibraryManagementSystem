using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Queries.BookQueries;

public class GetAllBorrowedBooksQuery : IRequest<List<Book>>
{ }
