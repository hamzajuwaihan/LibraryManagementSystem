using MediatR;

namespace ManagementLibrarySystem.Application.Commands.BookCommands;

public class ReturnBookCommand(Guid id) : IRequest<string>
{
    public Guid Id { get; } = id;
}