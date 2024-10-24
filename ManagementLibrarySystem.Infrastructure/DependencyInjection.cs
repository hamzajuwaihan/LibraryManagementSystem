using ManagementLibrarySystem.Infrastructure.EFCore;
using ManagementLibrarySystem.Infrastructure.Repositories;
using ManagementLibrarySystem.Infrastructure.RepositoriesContracts;
using Microsoft.Extensions.DependencyInjection;


namespace ManagementLibrarySystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){

        services.AddInfrastructureEFCore();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }

}
