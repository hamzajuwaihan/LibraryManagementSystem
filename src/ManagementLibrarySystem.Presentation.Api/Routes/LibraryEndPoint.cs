using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Application.Commands.LibraryMemberCommands;
using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Presentation.Api.Routes;

public static class LibraryEndPoint
{
    public static void MapLibraryEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("Library");

        group.MapPost("", async (AddLibraryCommand command, IMediator _mediator) =>
        {

            if (command == null) return Results.BadRequest("Library data is required");

            Library result = await _mediator.Send(command);

            return Results.Created($"/{result.Id}", result);
        })
        .WithTags("Library")
        .Produces<Library>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<AddLibraryCommand>("application/json");

        group.MapDelete("{id:guid}", async (Guid id, IMediator _mediator) =>
        {
            DeleteLibraryCommand command = new(id);

            bool result = await _mediator.Send(command);

            return Results.NoContent();

        })
        .WithTags("Library")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {

            GetLibraryByIdQuery query = new(id);

            Library book = await _mediator.Send(query);

            return Results.Ok(book);
        })
        .WithTags("Library")
        .Produces<Library>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator _mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            GetAllLibrariesQuery query = new() { PageNumber = pageNumber, PageSize = pageSize };

            List<Library> libraries = await _mediator.Send(query);

            return Results.Ok(libraries);
        })
        .WithTags("Library")
        .Produces<List<Library>>(StatusCodes.Status200OK);


        group.MapPost("{libraryId:guid}/add-library-member/{memberId:guid}", async (Guid libraryId, Guid memberId, IMediator _mediator) =>
        {

            bool result = await _mediator.Send(new AddLibraryMemberCommand(libraryId, memberId));

            if (!result) return Results.BadRequest("Failed to add member to the library. Either the library or member does not exist, or the member is already added.");

            return Results.Created($"/api/member/add-library-member", new
            {
                message = "Member added to library successfully."
            });
        })
        .WithTags("Library")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("{libraryId:guid}/remove-library-member/{memberId:guid}", async (Guid libraryId, Guid memberId, IMediator _mediator) =>
        {

            bool result = await _mediator.Send(new RemoveLibraryMemberCommand(libraryId, memberId));

            if (!result) return Results.BadRequest("Failed to remove member from the library. Either the library or member does not exist, or the member is not in the library.");

            return Results.Ok(new
            {
                message = "Member removed from library successfully."
            });
        })
        .WithTags("Library")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
