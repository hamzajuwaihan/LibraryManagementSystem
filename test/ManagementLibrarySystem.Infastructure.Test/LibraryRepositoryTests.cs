using ManagementLibrarySystem.Domain.Entities;
using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using ManagementLibrarySystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementLibrarySystem.Infastructure.Test;

public class LibraryRepositoryTests
{
    private DbAppContext CreateDbContext()
    {
        DbContextOptions<DbAppContext> options = new DbContextOptionsBuilder<DbAppContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DbAppContext(options);
    }

     #region AddingLibraryTests
    [Fact]
    public async Task CreateLibrary_ShouldAddLibraryToDatabase()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Library library = new(Guid.NewGuid())
        {
            Name = "Central Library",
        };

        Library createdLibrary = await repository.AddLibrary(library);

        Assert.NotNull(createdLibrary);
        Assert.Equal("Central Library", createdLibrary.Name);
        Assert.Single(context.Libraries);
    }

    [Fact]
    public async Task CreateLibrary_ShouldReturnExceptionWhenLibraryIsNull()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Library newLibrary = null!;

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddLibrary(newLibrary));
    }
    #endregion

    #region GetLibraryTests
    [Fact]
    public async Task GetLibraryById_ShouldReturnLibrarySuccessfully()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Guid libraryId = Guid.NewGuid();

        Library library = new(libraryId)
        {
            Name = "Central Library",
        };

        Library createdLibrary = await repository.AddLibrary(library);
        Library? searchLibrary = await repository.GetLibraryById(createdLibrary.Id);

        Assert.Equal(createdLibrary.Id, searchLibrary?.Id);
        Assert.Equal(createdLibrary.Name, searchLibrary?.Name);

    }

    [Fact]
    public async Task GetLibraryById_ShouldReturnNullWhenLibraryNotFound()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Guid randomGuid = Guid.NewGuid();

        Library? searchLibrary = await repository.GetLibraryById(randomGuid);

        Assert.Null(searchLibrary);
    }
    #endregion

    #region DeleteLibraryTests
    [Fact]
    public async Task DeleteLibraryById_ShouldReturnTrue()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Library library = new(Guid.NewGuid())
        {
            Name = "Central Library",
        };
        Library addedLibrary = await repository.AddLibrary(library);

        Assert.True(await repository.DeleteLibraryById(addedLibrary.Id));
        Assert.Empty(await repository.GetAllLibraries());
    }

    [Fact]
    public async Task DeleteLibraryById_ShouldReturnFalseWhenIdIsNull()
    {
        using DbAppContext context = CreateDbContext();
        LibraryRepository repository = new(context);

        Guid id = Guid.Empty;

        Assert.False(await repository.DeleteLibraryById(id));
    }
    #endregion
}
