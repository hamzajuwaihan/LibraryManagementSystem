using FluentValidation;
using FluentValidation.Results;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Application.Queries.BookQueries;
using ManagementLibrarySystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Presentation.Api.Routes;
public static class BooksEndPoint
{
    public static void MapBooksEndpoint(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/book");

        group.MapPost("", async (AddBookCommand command, IValidator<AddBookCommand> validator, IMediator _mediator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            Book result = await _mediator.Send(command);
            return Results.Created($"/book/{result.Id}", result);
        })
        .WithTags("Book")
        .Produces<Book>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<AddBookCommand>("application/json");

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {

            GetBookByIdQuery query = new(id);

            Book book = await _mediator.Send(query);

            if (book is null) return Results.NotFound();

            return Results.Ok(book);
        })
        .WithTags("Book")
        .Produces<Book>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);


        group.MapDelete("{id:guid}", async (Guid id, IValidator<DeleteBookCommand> validator, IMediator _mediator) =>
        {
            DeleteBookCommand command = new DeleteBookCommand(id);

            ValidationResult validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);


            bool result = await _mediator.Send(command);

            if (!result) return Results.NotFound();

            return Results.NoContent();
        })
        .WithTags("Book")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator _mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            GetAllBooksQuery query = new() { PageNumber = pageNumber, PageSize = pageSize };

            List<Book> books = await _mediator.Send(query);

            return Results.Ok(books);
        })
        .WithTags("Book")
        .Produces<List<Book>>(StatusCodes.Status200OK);

        group.MapPost("/{id}/borrow", async (Guid id, BorrowBookCommand request, IValidator<BorrowBookCommand> validator, IMediator mediator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid) return Results.BadRequest(new { message = "Validation failed", errors = validationResult.Errors });

            string result = await mediator.Send(request);

            return Results.Ok(new { message = result });
        })
        .WithTags("Book");



        group.MapPost("/{id}/return", async (Guid id, ReturnBookCommand command, IMediator mediator) =>
        {
            if (id != command.Id) return Results.BadRequest("Book ID in URL does not match the provided Book ID.");

            string result = await mediator.Send(command);

            return Results.Ok(result);
        })
        .WithTags("ReturnBook");

        group.MapGet("/borrowed", async (IMediator mediator, int pageNumber = 1, int pageSize = 10) =>
        {
            List<Book> result = await mediator.Send(new GetAllBorrowedBooksQuery() { PageNumber = pageNumber, PageSize = pageSize });
            return Results.Ok(result);
        })
        .WithTags("Book");


        group.MapPatch("/{id}", async (Guid id, PatchBookByIdCommand command, IValidator<PatchBookByIdCommand> validator, IMediator mediator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            Book patchedBook = await mediator.Send(command);
            return Results.Ok(patchedBook);
        })
        .WithTags("Book");
    }
}
