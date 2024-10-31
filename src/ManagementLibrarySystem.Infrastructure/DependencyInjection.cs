using ManagementLibrarySystem.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using ManagementLibrarySystem.Infrastructure.Repositories;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.Extensions.DependencyInjection;



namespace ManagementLibrarySystem.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Depenedncy Injection to add infastructure to program.cs, while implcitly adding EF core layer 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        string? host = Environment.GetEnvironmentVariable("DB_HOST");
        string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
        string? user = Environment.GetEnvironmentVariable("DB_USER");
        string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        string? port = Environment.GetEnvironmentVariable("DB_PORT");

        string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";

        services.AddDbContext<DbAppContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
