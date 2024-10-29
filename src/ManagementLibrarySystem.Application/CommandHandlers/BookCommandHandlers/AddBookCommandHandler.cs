using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;

/// <summary>
/// Handler for MediatR to add a new book to the database
/// </summary>
/// <param name="bookRepository"></param>
/// <param name="libraryRepository"></param>
public class AddBookCommandHandler(IBookRepository bookRepository, ILibraryRepository libraryRepository) : IRequestHandler<AddBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    /// <summary>
    /// Implement handler function for MediatR
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Library? libary = await _libraryRepository.GetLibraryById(request.LibraryId) ?? throw new Exception($"Library with ID {request.LibraryId} does not exist.");

            Book newbook = new(Guid.NewGuid())
            {
                Title = request.Title,
                Author = request.Author,
                BorrowedBy = null,
                LibraryId = libary.Id
            };

            return await _bookRepository.AddBook(newbook);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
