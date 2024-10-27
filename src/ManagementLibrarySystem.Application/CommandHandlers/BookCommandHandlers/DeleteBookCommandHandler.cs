using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;

public class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _bookRepository.DeleteBookById(request.BookId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

    }
}
