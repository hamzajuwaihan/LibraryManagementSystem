
using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.LibraryQueryHandlersTests;

public class GetAllLibrariesQueryHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly GetAllLibrariesQueryHandler _handler;

    public GetAllLibrariesQueryHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new GetAllLibrariesQueryHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task GetAllLibrariesQueryHandler_WhenLibrariesExist_ReturnsLibraries()
    {
        List<Library> libraries = new()
        {
            new Library(Guid.NewGuid()) { Name = "Library One" },
            new Library(Guid.NewGuid()) { Name = "Library Two" }
        };

        _mockLibraryRepository.Setup(repo => repo.GetAllLibraries()).ReturnsAsync(libraries);

        GetAllLibrariesQuery query = new();


        List<Library> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Library One", result[0].Name);
        Assert.Equal("Library Two", result[1].Name);
    }

    [Fact]
    public async Task GetAllLibrariesQueryHandler_WhenNoLibrariesExist_ReturnsEmptyList()
    {
        _mockLibraryRepository.Setup(repo => repo.GetAllLibraries()).ReturnsAsync(new List<Library>());

        GetAllLibrariesQuery query = new();


        List<Library> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result); 
    }

    [Fact]
    public async Task GetAllLibrariesQueryHandler_WhenExceptionThrown_ReturnsEmptyList()
    {
        _mockLibraryRepository.Setup(repo => repo.GetAllLibraries()).ThrowsAsync(new Exception("Database error"));

        GetAllLibrariesQuery query = new();


        List<Library> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result); 
    }
}
