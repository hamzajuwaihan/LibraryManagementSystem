using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Domain.Exceptions.Common;
using ManagementLibrarySystem.Domain.Exceptions.Library;
using ManagementLibrarySystem.Domain.Exceptions.Member;

namespace ManagementLibrarySystem.Http.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BookNotFoundException ex)
        {
            _logger.LogError(ex, "Book not found.");
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (BookAlreadyBorrowedException ex)
        {
            _logger.LogError(ex, "Book already borrowed.");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (MemberNotFoundException ex)
        {
            _logger.LogError(ex, "Member not found.");
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (LibraryNotFoundException ex)
        {
            _logger.LogError(ex, "Library not found.");
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (DuplicateEmailException ex)
        {
            _logger.LogError(ex, "Duplicate Email.");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (DuplicateLibraryNameException ex)
        {
            _logger.LogError(ex, "Duplicate Library Name");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (BookIsNotCurrentlyBorrowedException ex)
        {
            _logger.LogError(ex, "You cannot return a book that is not borrowed.");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (LibraryAlreadyHasThisMemberException ex)
        {
            _logger.LogError(ex, "The member is already in the provided library.");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (InvalidPatchOperationException ex)
        {
            _logger.LogError(ex, "Invalid patch operation.");
            await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (LibraryDoesNotHaveThisMemberException ex)
        {
            _logger.LogError(ex, "Library does not have this member.");
            await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;


        return context.Response.WriteAsJsonAsync(new
        {
            message
        });
    }
}
