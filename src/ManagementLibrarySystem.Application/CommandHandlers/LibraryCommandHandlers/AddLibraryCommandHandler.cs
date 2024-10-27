using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using MediatR;

namespace ManagementLibrarySystem.Application.CommandHandlers.LibraryCommandHandlers;

public class AddLibraryCommandHandler(ILibraryRepository libraryRepository) : IRequestHandler<AddLibraryCommand, Library>
{
    private readonly ILibraryRepository _libraryRepository = libraryRepository;
    public async Task<Library> Handle(AddLibraryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Library newLibrary = new(Guid.NewGuid())
            {
                Name = request.Name,
            };

            return await _libraryRepository.AddLibrary(newLibrary);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
