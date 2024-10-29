
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Update Book By Id command handler
/// </summary>
/// <param name="bookRepository"></param>
public class UpdateBookByIdCommandHandler(IBookRepository bookRepository) : IRequestHandler<UpdateBookByIdCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handler function implementation 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
    {
        Book book = await _bookRepository.GetBookById(request.BookId) ?? throw new KeyNotFoundException("Book not found");

        book.Update(request.Title, request.Author, request.IsBorrowed, request.BorrowedDate, request.BorrowedBy);

        await _bookRepository.UpdateBookById(request.BookId, book);

        return book;
    }
}
