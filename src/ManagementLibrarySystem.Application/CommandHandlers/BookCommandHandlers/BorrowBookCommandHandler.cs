using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, string>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;

    public BorrowBookCommandHandler(IBookRepository bookRepository, IMemberRepository memberRepository)
    {
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public async Task<string> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookById(request.BookId);
        if (book == null)
            return "Book not found.";

        if (book.IsBorrowed)
            return "Book is already borrowed.";

        var member = await _memberRepository.GetMemberById(request.MemberId);
        if (member == null)
            return "Member not found.";

        book.Update(book.Title, book.Author, true, DateTime.UtcNow, request.MemberId);

        await _bookRepository.UpdateBookById(request.BookId, book);

        return "Book borrowed successfully.";
    }
}
