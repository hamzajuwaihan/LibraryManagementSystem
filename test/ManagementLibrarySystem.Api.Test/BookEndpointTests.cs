using FluentValidation;
using FluentValidation.Results;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using System.Net;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using ManagementLibrarySystem.Application.Queries.BookQueries;


namespace ManagementLibrarySystem.Api.Test;

public class BookEndpointTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IValidator<BorrowBookCommand>> _borrowValidatorMock;
    private readonly Mock<IValidator<AddBookCommand>> _addValidatorMock;

    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly HttpClient _client;

    public BookEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _borrowValidatorMock = new Mock<IValidator<BorrowBookCommand>>();
        _addValidatorMock = new Mock<IValidator<AddBookCommand>>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _memberRepositoryMock = new Mock<IMemberRepository>();

        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _mediatorMock.Object);
                    services.AddScoped(_ => _borrowValidatorMock.Object);
                    services.AddScoped(_ => _bookRepositoryMock.Object);
                    services.AddScoped(_ => _memberRepositoryMock.Object);
                });
            });

        _client = webAppFactory.CreateClient();
    }

    [Fact]
    public async Task BorrowBook_ReturnsOk_WhenCommandIsValid()
    {

        Guid bookId = Guid.NewGuid();
        Guid memberId = Guid.NewGuid();


        BorrowBookCommand command = new(memberId);


        _borrowValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<BorrowBookCommand>(), default))
            .ReturnsAsync(new ValidationResult());

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<BorrowBookCommand>(), default))
            .ReturnsAsync("Book borrowed successfully.");

        HttpResponseMessage response = await _client.PostAsJsonAsync($"/book/{bookId}/borrow", command);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task BorrowBook_ReturnsBadRequest_WhenValidationFails()
    {
        Guid bookId = Guid.NewGuid();
        BorrowBookCommand command = new(Guid.NewGuid());

        _borrowValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<BorrowBookCommand>(), default))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("BookId", "Invalid book ID.")
            }));

        HttpResponseMessage response = await _client.PostAsJsonAsync($"/book/{bookId}/borrow", command);


        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        string? result = await response.Content.ReadAsStringAsync();
        Assert.Contains("Invalid book ID.", result);
    }


    [Fact]
    public async Task AddBook_ReturnsBadRequest_WhenBookIsInvalid()
    {
        AddBookCommand invalidBookCommand = new AddBookCommand("", "", Guid.NewGuid());
        _addValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<AddBookCommand>(), default))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
            {
        new ValidationFailure("Title", "Title is required"),
        new ValidationFailure("Author", "Author is required")
            }));

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AddBookCommand>(), default))
            .ThrowsAsync(new Exception("Should not reach mediator for invalid command"));

        HttpResponseMessage response = await _client.PostAsJsonAsync("/book", invalidBookCommand);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        string result = await response.Content.ReadAsStringAsync();


        using JsonDocument doc = JsonDocument.Parse(result);
        JsonElement root = doc.RootElement;

        Assert.True(root.ValueKind == JsonValueKind.Array, "Expected a JSON array.");

        List<string> errors = new List<string>();
        foreach (JsonElement error in root.EnumerateArray())
        {
            string propertyName = error.GetProperty("propertyName").GetString()!;
            string errorMessage = error.GetProperty("errorMessage").GetString()!;

            errors.Add($"{propertyName}: {errorMessage}");
        }

        Assert.Contains(errors, e => e == "Title: Title is required");
        Assert.Contains(errors, e => e == "Author: Author is required");
    }

    [Fact]
    public async Task GetBookById_ReturnsBook_WhenBookExists()
    {
        Guid bookId = Guid.NewGuid();
        Book expectedBook = new Book(bookId) { Title = "Test Book", Author = "Test Author" };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
            .ReturnsAsync(expectedBook);

        HttpResponseMessage response = await _client.GetAsync($"/book/{bookId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string? result = await response.Content.ReadAsStringAsync();
        Assert.Contains("Test Book", result);
        Assert.Contains("Test Author", result);
    }

    [Fact]
    public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
    {
        Guid bookId = Guid.NewGuid();
        HttpResponseMessage response = await _client.GetAsync($"/book/{bookId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
    {
        Guid bookId = Guid.NewGuid();

        HttpResponseMessage response = await _client.DeleteAsync($"/book/{bookId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Fact]
    public async Task AddBook_ReturnsCreated_WhenBookIsValid()
    {

        Guid libraryId = Guid.NewGuid();

        AddBookCommand command = new("New Book", "Author", libraryId);


        Book createdBook = new Book(Guid.NewGuid())
        {
            Title = "New Book",
            Author = "Author",
            LibraryId = libraryId
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AddBookCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdBook);

        HttpResponseMessage response = await _client.PostAsJsonAsync("/book", command);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Book? resultBook = await response.Content.ReadFromJsonAsync<Book>();
        Assert.NotNull(resultBook);
        Assert.Equal("New Book", resultBook.Title);
        Assert.Equal(libraryId, resultBook.LibraryId);
    }

    [Fact]
    public async Task DeleteBook_ReturnsNoContent_WhenBookExists()
    {
        Guid bookId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);


        HttpResponseMessage response = await _client.DeleteAsync($"/book/{bookId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task ReturnBook_ReturnsBadRequest_WhenBookIdMismatch()
    {
        Guid bookId = Guid.NewGuid();
        ReturnBookCommand command = new(bookId);

        HttpResponseMessage response = await _client.PostAsJsonAsync($"/book/{Guid.NewGuid()}/return", command);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task ReturnBook_ReturnsOk_WhenBookCanBeReturned()
    {

        Guid bookId = Guid.NewGuid();

        _mediatorMock
             .Setup(m => m.Send(It.IsAny<ReturnBookCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync("Book returned successfully.");


        ReturnBookCommand returnCommand = new(bookId);


        HttpResponseMessage returnResponse = await _client.PostAsJsonAsync($"/book/{bookId}/return", returnCommand);


        Assert.Equal(HttpStatusCode.OK, returnResponse.StatusCode);
        string returnResultMessage = await returnResponse.Content.ReadAsStringAsync();
        Assert.Equal("\"Book returned successfully.\"", returnResultMessage);
    }


    [Fact]
    public async Task PatchBook_ReturnsOk_WhenBookIsPatchedSuccessfully()
    {
        Guid targetId = Guid.NewGuid();

        Book originalBook = new Book(targetId)
        {
            Title = "Original Title",
            Author = "Original Author"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(originalBook);

        Book updatedBook = new Book(targetId)
        {
            Title = "Updated Title",
            Author = "Original Author"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<PatchBookByIdCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedBook);

        PatchBookByIdCommand patchCommand = new PatchBookByIdCommand("Updated Title", "Original Author", null, null, null);

        HttpResponseMessage response = await _client.PatchAsJsonAsync($"/book/{targetId}", patchCommand);

        response.EnsureSuccessStatusCode();

        Book? patchedBook = await response.Content.ReadFromJsonAsync<Book>();

        Assert.NotNull(patchedBook);
        Assert.Equal(targetId, patchedBook.Id);
        Assert.Equal("Updated Title", patchedBook.Title);
        Assert.Equal("Original Author", patchedBook.Author);
    }

}
