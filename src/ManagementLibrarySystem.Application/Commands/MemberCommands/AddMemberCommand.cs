using ManagementLibrarySystem.Domain.Entities;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.MemberCommands;

public class AddMemberCommand(string name, string email) : IRequest<Member>
{
    public string Name { get; } = name;
    public string Email { get; } = email;

}
