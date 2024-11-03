using MediatR;

namespace ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;

public class RemoveLibraryMemberCommand(Guid libraryId, Guid memberId) : IRequest<bool>
{
    public Guid LibraryId { get; } = libraryId;
    public Guid MemberId { get; } = memberId;
}
