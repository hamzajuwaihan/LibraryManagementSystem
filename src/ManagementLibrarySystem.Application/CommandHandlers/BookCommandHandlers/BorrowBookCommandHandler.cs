using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Handler implementation for MediatR to borrow a book for a certain member
/// </summary>
/// <param name="bookRepository"></param>
/// <param name="memberRepository"></param>
public class BorrowBookCommandHandler(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<BorrowBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


    /// <summary>
    /// Handler function to handler BorrowBookCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Book> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        Guid id = Guid.Parse(_httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString()!);
        Guid memberId = Guid.Parse(_httpContextAccessor.HttpContext?.GetRouteValue("memberId")?.ToString()!);

        return await _bookRepository.BorrowBook(id, memberId);
    }
}
