using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class DeleteBookCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; } = id;
}
