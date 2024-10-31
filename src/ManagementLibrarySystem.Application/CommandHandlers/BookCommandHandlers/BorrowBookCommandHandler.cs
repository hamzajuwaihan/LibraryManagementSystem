using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Domain.Exceptions.Member;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Handler implemetnation for MediatR to borrow a book for a certain member
/// </summary>
/// <param name="bookRepository"></param>
/// <param name="memberRepository"></param>
public class BorrowBookCommandHandler(IBookRepository bookRepository, IMemberRepository memberRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<BorrowBookCommand, string>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMemberRepository _memberRepository = memberRepository;

    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


    /// <summary>
    /// Hander function to handler BorrowBookCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        string id = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString()!;

        Guid bookId = Guid.Parse(id);

        Book? book = await _bookRepository.GetBookById(bookId);

        if (book!.IsBorrowed) throw new BookAlreadyBorrowedException();

        Member member = await _memberRepository.GetMemberById(request.MemberId);

        book.Borrow(member.Id);

        await _bookRepository.UpdateBook(bookId, book);

        return "Book borrowed successfully.";
    }
}
