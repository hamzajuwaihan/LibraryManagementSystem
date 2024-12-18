
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

        GetAllLibrariesQuery query = new();

        _mockLibraryRepository.Setup(repo => repo.GetAllLibraries(query.PageSize, query.PageNumber)).ReturnsAsync(libraries);

        List<Library> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Library One", result[0].Name);
        Assert.Equal("Library Two", result[1].Name);
    }

    [Fact]
    public async Task GetAllLibrariesQueryHandler_WhenNoLibrariesExist_ReturnsEmptyList()
    {
        GetAllLibrariesQuery query = new();

        _mockLibraryRepository.Setup(repo => repo.GetAllLibraries(query.PageSize, query.PageNumber)).ReturnsAsync([]);

        List<Library> result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result);
    }


}
