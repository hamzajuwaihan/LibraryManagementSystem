using ManagementLibrarySystem.Infrastructure.EFCore;
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

        services.AddInfrastructureEFCore();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
