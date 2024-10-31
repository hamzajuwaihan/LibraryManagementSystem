using ManagementLibrarySystem.Application.CommandHandlers.BookCommandHandlers;
using ManagementLibrarySystem.Application.Commands.BookCommands;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ManagementLibrarySystem.Application.Test.CommandHandlersTests.BookCommandHandlersTests;

public class PatchBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly PatchBookByIdCommandHandler _handler;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    public PatchBookCommandHandlerTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _handler = new PatchBookByIdCommandHandler(_mockBookRepository.Object, _mockHttpContextAccessor.Object);
    }

    [Fact]
    public async Task PatchBook_BookExists_PatchesBookSuccessfully()
    {
        Guid id = Guid.NewGuid();
        Book initialBookState = new(id) { Title = "Old Title", Author = "Old Author", IsBorrowed = false };

        _mockBookRepository.Setup(repo => repo.GetBookById(id)).ReturnsAsync(initialBookState);

        PatchBookByIdCommand command = new("New Title", null, null, null, null);

        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", id.ToString() }
        };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);


        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("New Title", result.Title);
        Assert.Equal("Old Author", result.Author);
        Assert.False(result.IsBorrowed);
        _mockBookRepository.Verify(repo => repo.PatchBookById(id, It.IsAny<Book>()), Times.Once);
    }


    [Fact]
    public async Task PatchBook_BookExists_OnlyUpdatesProvidedFields()
    {
        Guid id = Guid.NewGuid();
        Book initialBookState = new(id) { Title = "Old Title", Author = "Old Author", IsBorrowed = false };
        _mockBookRepository.Setup(repo => repo.GetBookById(id)).ReturnsAsync(initialBookState);
        PatchBookByIdCommand command = new(null, "New Author", null, null, null);
        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        mockHttpContext.Request.RouteValues = new RouteValueDictionary
        {
            { "id", id.ToString() }
        };

        _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(mockHttpContext);

        Book result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal("Old Title", result.Title);
        Assert.Equal("New Author", result.Author);
        Assert.False(result.IsBorrowed);
        _mockBookRepository.Verify(repo => repo.PatchBookById(id, It.IsAny<Book>()), Times.Once);
    }
}