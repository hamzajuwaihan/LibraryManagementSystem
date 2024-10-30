
using ManagementLibrarySystem.Application.Queries.LibraryQueries;
using ManagementLibrarySystem.Application.QueryHandlers.LibraryQueryHandlers;
using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Domain.Exceptions.Library;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;

namespace ManagementLibrarySystem.Application.Test.QueryHandlersTests.LibraryQueryHandlersTests;

public class GetLibraryByIdQueryHandlerTests
{
    private readonly Mock<ILibraryRepository> _mockLibraryRepository;
    private readonly GetLibraryByIdQueryHandler _handler;

    public GetLibraryByIdQueryHandlerTests()
    {
        _mockLibraryRepository = new Mock<ILibraryRepository>();
        _handler = new GetLibraryByIdQueryHandler(_mockLibraryRepository.Object);
    }

    [Fact]
    public async Task GetLibraryByIdQueryHandler_WhenLibraryExists_ReturnsLibrary()
    {
        Guid libraryId = Guid.NewGuid();
        Library expectedLibrary = new Library(libraryId) { Name = "Test Library" };

        _mockLibraryRepository.Setup(repo => repo.GetLibraryById(libraryId)).ReturnsAsync(expectedLibrary);

        GetLibraryByIdQuery query = new GetLibraryByIdQuery(libraryId);

        Library? result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(expectedLibrary.Name, result?.Name);
    }

    [Fact]
    public async Task GetLibraryByIdQueryHandler_WhenLibraryDoesNotExist_ReturnsNull()
    {
        Guid libraryId = Guid.NewGuid();

        _mockLibraryRepository.Setup(repo => repo.GetLibraryById(libraryId)).ReturnsAsync((Library?)null);

        GetLibraryByIdQuery query = new GetLibraryByIdQuery(libraryId);

        await Assert.ThrowsAsync<LibraryNotFoundException>(()=> _handler.Handle(query, CancellationToken.None));
    }


}
