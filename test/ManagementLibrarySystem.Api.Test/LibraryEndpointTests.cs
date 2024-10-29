using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Application.Commands.LibraryCommands;
using ManagementLibrarySystem.Application.Queries.LibraryQueries;


namespace ManagementLibrarySystem.Api.Test;

public class LibraryEndpointTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly HttpClient _client;

    public LibraryEndpointTests()
    {
        _mediatorMock = new Mock<IMediator>();

        WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => _mediatorMock.Object);
                });
            });

        _client = webAppFactory.CreateClient();
    }

    [Fact]
    public async Task Post_Library_ReturnsCreated()
    {
        AddLibraryCommand newLibrary = new AddLibraryCommand("New Library");

        Library createdLibrary = new Library(Guid.NewGuid()) { Name = "New Library" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddLibraryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdLibrary);

        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/library", newLibrary);

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        Library? returnedLibrary = await response.Content.ReadFromJsonAsync<Library>();
        Assert.NotNull(returnedLibrary);
        Assert.Equal(createdLibrary.Id, returnedLibrary.Id);
    }

    [Fact]
    public async Task Delete_Library_ReturnsNoContent()
    {
        Guid libraryId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteLibraryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        HttpResponseMessage response = await _client.DeleteAsync($"/api/library/{libraryId}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
    [Fact]
    public async Task Get_LibraryById_ReturnsOk()
    {
        Guid libraryId = Guid.NewGuid();
        Library library = new Library(libraryId) { Name = "Existing Library" };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetLibraryByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(library);

        HttpResponseMessage response = await _client.GetAsync($"/api/library/{libraryId}");

        response.EnsureSuccessStatusCode();
        Library? returnedLibrary = await response.Content.ReadFromJsonAsync<Library>();
        Assert.NotNull(returnedLibrary);
        Assert.Equal(libraryId, returnedLibrary.Id);
        Assert.Equal(library.Name, returnedLibrary.Name);
    }

    [Fact]
    public async Task Get_AllLibraries_ReturnsOk()
    {
        List<Library> libraries = new List<Library>
            {
                new Library(Guid.NewGuid()) {  Name = "Library 1" },
                new Library(Guid.NewGuid()) {  Name = "Library 2" }
            };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllLibrariesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(libraries);

        HttpResponseMessage response = await _client.GetAsync("/api/library");

        response.EnsureSuccessStatusCode();
        List<Library>? returnedLibraries = await response.Content.ReadFromJsonAsync<List<Library>>();
        Assert.NotNull(returnedLibraries);
        Assert.Equal(2, returnedLibraries.Count);
    }


}
