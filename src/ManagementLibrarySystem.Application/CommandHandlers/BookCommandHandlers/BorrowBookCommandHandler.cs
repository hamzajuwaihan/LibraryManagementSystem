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
        string? BookId = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

        if (BookId == null || !Guid.TryParse(BookId, out Guid bookToBorrow)) throw new ArgumentException("Invalid or missing 'id' in route.");

        Book? book = await _bookRepository.GetBookById(bookToBorrow) ?? throw new BookNotFoundException();

        if (book!.IsBorrowed) throw new BookAlreadyBorrowedException();

        Member? member = await _memberRepository.GetMemberById(request.MemberId) ?? throw new MemberNotFoundException();

        book.Update(book.Title, book.Author, true, DateTime.UtcNow, member.Id);

        await _bookRepository.UpdateBookById(bookToBorrow, book);

        return "Book borrowed successfully.";
    }
}
