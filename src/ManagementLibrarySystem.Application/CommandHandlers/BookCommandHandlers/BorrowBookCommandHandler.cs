using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Handler implemetnation for MediatR to borrow a book for a certain member
/// </summary>
/// <param name="bookRepository"></param>
/// <param name="memberRepository"></param>
public class BorrowBookCommandHandler(IBookRepository bookRepository, IMemberRepository memberRepository) : IRequestHandler<BorrowBookCommand, string>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMemberRepository _memberRepository = memberRepository;

    /// <summary>
    /// Hander function to handler BorrowBookCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetBookById(request.BookId);

        if (book is null) return "Book not found.";

        if (book.IsBorrowed) return "Book is already borrowed.";

        Member? member = await _memberRepository.GetMemberById(request.MemberId);

        if (member is null) return "Member not found.";

        book.Update(book.Title, book.Author, true, DateTime.UtcNow, request.MemberId);

        await _bookRepository.UpdateBookById(request.BookId, book);

        return "Book borrowed successfully.";
    }
}
