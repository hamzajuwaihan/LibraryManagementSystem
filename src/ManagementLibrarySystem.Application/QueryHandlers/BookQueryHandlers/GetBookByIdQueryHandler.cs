using System;
using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.QueryHandlers.BookQueryHandlers;

public class GetBookByIdQueryHandler(IBookRepository bookRepository) : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Book? result = await _bookRepository.GetBookById(request.BookId);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
