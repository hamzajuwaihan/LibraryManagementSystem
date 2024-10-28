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
        RouteGroupBuilder group = app.MapGroup("api/book");

        group.MapPost("", async (AddBookCommand command, IValidator<AddBookCommand> validator, IMediator _mediator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

            if (command == null) return Results.BadRequest("Book data is required");

            Book result = await _mediator.Send(command);
            return Results.Created($"/api/books/{result.Id}", result);
        })
        .WithTags("Books")
        .Produces<Book>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Accepts<AddBookCommand>("application/json");

        group.MapGet("{id:Guid}", async (Guid id, IMediator _mediator) =>
        {

            GetBookByIdQuery query = new(id);

            Book book = await _mediator.Send(query);

            if (book == null) return Results.NotFound();

            return Results.Ok(book);
        })
        .WithTags("Books")
        .Produces<Book>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);


        group.MapDelete("{id:guid}", async (Guid id, IMediator _mediator) =>
        {
            DeleteBookCommand command = new(id);

            bool result = await _mediator.Send(command);

            if (!result) return Results.NotFound();

            return Results.NoContent();

        })
        .WithTags("Books")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("", async (IMediator _mediator) =>
        {
            GetAllBooksQuery query = new GetAllBooksQuery();

            List<Book> books = await _mediator.Send(query);

            return Results.Ok(books);
        })
        .WithTags("Books")
        .Produces<List<Book>>(StatusCodes.Status200OK);

        app.MapPost("/books/{bookId}/borrow", async (Guid bookId, BorrowBookCommand request, IMediator mediator) =>
        {
            if (request.BookId != bookId) return Results.BadRequest("Book ID and Request book ID are not the same");

            BorrowBookCommand command = new BorrowBookCommand(bookId, request.MemberId);
            string result = await mediator.Send(command);

            return Results.Ok(result);
        })
        .WithTags("Borrow Book");

        app.MapPost("/books/{bookId}/return", async (Guid bookId, ReturnBookCommand command, IMediator mediator) =>
        {
            if (bookId != command.BookId) return Results.BadRequest("Book ID in URL does not match the provided Book ID.");

            string result = await mediator.Send(command);

            return Results.Ok(result);
        })
        .WithTags("ReturnBook");

        app.MapGet("/books/borrowed", async (IMediator mediator) =>
        {
            List<Book> result = await mediator.Send(new GetAllBorrowedBooksQuery());
            return Results.Ok(result);
        })
        .WithTags("GetAllBorrowedBooks");

        app.MapPut("/books/{bookId}", async (Guid bookId, UpdateBookByIdCommand command, IMediator mediator) =>
        {
            if (command.BookId != bookId) return Results.BadRequest("Book ID in path and command do not match.");
            
            Book updatedBook = await mediator.Send(command);

            return Results.Ok(updatedBook);
        })
        .WithTags("Books");

        app.MapPatch("/books/{bookId}", async (Guid bookId, PatchBookByIdCommand command, IMediator mediator) =>
        {
            if (command.BookId != bookId) return Results.BadRequest("Book ID in path and command do not match.");

            Book patchedBook = await mediator.Send(command);
            
            return Results.Ok(patchedBook);
        })
        .WithTags("Books");
    }
}
