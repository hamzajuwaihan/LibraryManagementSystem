using ManagementLibrarySystem.Presentation.Api.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace ManagementLibrarySystem.Presentation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddBookCommandValidator>();


        return services;
    }

}
