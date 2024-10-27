using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, string>
{
    private readonly IBookRepository _bookRepository;

    public ReturnBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<string> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookById(request.BookId);
        if (book == null)
            return "Book not found.";

        if (!book.IsBorrowed)
            return "Book is not currently borrowed.";

        
        book.Update(book.Title, book.Author, false, null, null);

        await _bookRepository.UpdateBookById(request.BookId, book);

        return "Book returned successfully.";
    }
}
