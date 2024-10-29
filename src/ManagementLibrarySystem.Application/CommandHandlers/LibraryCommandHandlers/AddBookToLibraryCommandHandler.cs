using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;
/// <summary>
/// Add Book To library Comamnd Handler class
/// </summary>
/// <param name="libraryRepository"></param>
public class AddBookToLibraryCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<AddBookToLibraryCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
/// <summary>
/// Implementation for IReuqestHandler
/// </summary>
/// <param name="request"></param>
/// <param name="cancellationToken"></param>
/// <returns></returns>
    public async Task<bool> Handle(AddBookToLibraryCommand request, CancellationToken cancellationToken)
    {
        Library? library = await _libraryRepository.GetLibraryById(request.LibraryId);
        
        if (library is null) return false;
        
        Book book = new(request.BookId)
        {
            Title = "New Book Title",
            Author = "Author Name",
        };

        library.Books.Add(book);

        await _libraryRepository.UpdateLibraryById(library);

        return true;
    }
}
