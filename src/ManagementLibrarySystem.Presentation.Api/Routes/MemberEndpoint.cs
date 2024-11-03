using ManagementLibrarySystem.Application.Commands.MemberCommands;
using ManagementLibrarySystem.Application.Queries.MemberQueries;
using ManagementLibrarySystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Presentation.Api.Routes;

public static class MemberEndpoint
{
    public static void MapMemberEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("member");

        group.MapPost("", async (AddMemberCommand command, IMediator _mediator) =>
        {

            if (command == null) return Results.BadRequest("Library data is required");

            Member result = await _mediator.Send(command);

            return Results.Created($"member/{result.Id}", result);
        })
        .WithTags("Member")
        .Produces<Library>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<AddMemberCommand>("application/json");

        group.MapDelete("{id:guid}", async (Guid id, IMediator _mediator) =>
        {
            DeleteMemberCommand command = new(id);

            bool result = await _mediator.Send(command);

            return Results.NoContent();

        })
        .WithTags("Member")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {

            GetMemberByIdQuery query = new(id);

            Member member = await _mediator.Send(query);

            return Results.Ok(member);
        })
        .WithTags("Member")
        .Produces<Book>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator _mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            GetAllMembersQuery query = new() { PageNumber = pageNumber, PageSize = pageSize };

            List<Member> members = await _mediator.Send(query);

            return Results.Ok(members);
        })
        .WithTags("Member")
        .Produces<List<Member>>(StatusCodes.Status200OK);

        group.MapPatch("{id:guid}", async (Guid id, PatchMemberCommand command, IMediator _mediator) =>
        {
            Member result = await _mediator.Send(command);

            return Results.Ok(result);
        })
        .WithTags("Member")
        .Accepts<PatchMemberCommand>("application/json")
        .Produces<Member>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
