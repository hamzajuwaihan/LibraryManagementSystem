using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;

public class AddBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<AddBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Book newbook = new Book(Guid.NewGuid())
            {
                Title = request.Title,
                Author = request.Author
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
