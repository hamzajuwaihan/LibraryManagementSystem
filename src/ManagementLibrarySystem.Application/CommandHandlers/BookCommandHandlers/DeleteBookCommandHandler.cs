using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
/// <summary>
/// Impelmentation for IReuqest handler to hande Delete Book Command
/// </summary>
/// <param name="bookRepository"></param>
public class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    /// <summary>
    /// Handler function for the MediatR
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
