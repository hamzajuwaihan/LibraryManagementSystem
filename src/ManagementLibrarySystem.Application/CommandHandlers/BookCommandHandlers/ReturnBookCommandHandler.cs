using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Return Book Command handler
/// </summary>
/// <param name="bookRepository"></param>
public class ReturnBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<ReturnBookCommand, string>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handle function
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        Book book = await _bookRepository.GetBookById(request.Id);

        if (!book.IsBorrowed) throw new BookIsNotCurrentlyBorrowedException();

        book.Return();

        await _bookRepository.UpdateBook(request.Id, book);

        return "Book returned successfully.";
    }
}
