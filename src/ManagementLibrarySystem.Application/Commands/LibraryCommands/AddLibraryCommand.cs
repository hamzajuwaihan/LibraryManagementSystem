using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.LibraryCommands;

public class AddLibraryCommand(string name) : IRequest<Library>
{
    public string Name { get; } = name;

}
