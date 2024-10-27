using System;
using MediatR;

namespace ManagementLibrarySystem.Application.Commands.MemberCommands;

public class DeleteMemberCommand(Guid id) : IRequest<bool>
{
    public Guid MemberId { get; } = id;
}
