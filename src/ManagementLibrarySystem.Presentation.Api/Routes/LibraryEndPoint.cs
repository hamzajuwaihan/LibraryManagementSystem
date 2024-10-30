using ManagementLibrarySystem.Application.Commands.LibraryCommands;
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

            if (!result) return Results.NotFound();

            return Results.NoContent();

        })
        .WithTags("Library")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {

            GetLibraryByIdQuery query = new(id);

            Library book = await _mediator.Send(query);

            if (book == null) return Results.NotFound();

            return Results.Ok(book);
        })
        .WithTags("Library")
        .Produces<Book>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async(IMediator _mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            GetAllLibrariesQuery query = new() { PageNumber = pageNumber, PageSize = pageSize }; ;
            List<Library> libraries = await _mediator.Send(query);

            return Results.Ok(libraries);
        })
        .WithTags("Library")
        .Produces<List<Book>>(StatusCodes.Status200OK);



    }
}
