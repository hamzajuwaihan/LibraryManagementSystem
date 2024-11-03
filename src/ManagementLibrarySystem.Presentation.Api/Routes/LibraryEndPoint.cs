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
            if (string.IsNullOrEmpty(command.Name)) return Results.BadRequest("Library name is required");

            Library result = await _mediator.Send(command);

            return Results.Created($"/{result.Id}", result);
        })
        .WithTags("Library")
        .Produces<Library>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<AddLibraryCommand>("application/json");

        group.MapDelete("{id:guid}", async (Guid id, IMediator _mediator) =>
        {
            await _mediator.Send(new DeleteLibraryCommand(id));

            return Results.NoContent();
        })
        .WithTags("Library")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {
            Library book = await _mediator.Send(new GetLibraryByIdQuery(id));

            return Results.Ok(book);
        })
        .WithTags("Library")
        .Produces<Library>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator _mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            List<Library> libraries = await _mediator.Send(new GetAllLibrariesQuery(){
                PageNumber = pageNumber, 
                PageSize = pageSize 
            });

            return Results.Ok(libraries);
        })
        .WithTags("Library")
        .Produces<List<Library>>(StatusCodes.Status200OK);


        group.MapPost("{libraryId:guid}/add-library-member/{memberId:guid}", async (Guid libraryId, Guid memberId, IMediator _mediator) =>
        {
            bool result = await _mediator.Send(new AddLibraryMemberCommand(libraryId, memberId));

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
