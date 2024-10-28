using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;

public class AddBookToLibraryCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<AddBookToLibraryCommand, bool>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;

    public async Task<bool> Handle(AddBookToLibraryCommand request, CancellationToken cancellationToken)
    {
        Library? library = await _libraryRepository.GetLibraryById(request.LibraryId);
        
        if (library == null) return false;
        
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
