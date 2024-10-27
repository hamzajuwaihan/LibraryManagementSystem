using MediatR;

namespace ManagementLibrarySystem.Application.Commands.LibraryCommands;

public class DeleteLibraryCommand(Guid id) : IRequest<bool>
{
    public Guid LibraryId { get; } = id;
}
