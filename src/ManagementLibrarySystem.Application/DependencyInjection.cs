using Microsoft.Extensions.DependencyInjection;
using MediatR;
using ManagementLibrarySystem.Application.Commands.BookCommands;

namespace ManagementLibrarySystem.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Depenency injection to add application layer to program.cs
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddBookCommand).Assembly));

        return services;
    }

}
