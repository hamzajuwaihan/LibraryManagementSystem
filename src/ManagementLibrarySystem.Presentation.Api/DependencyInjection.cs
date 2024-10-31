using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using ManagementLibrarySystem.Presentation.Api.Validators;

namespace ManagementLibrarySystem.Presentation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddBookCommandValidator>();

        return services;
    }

}
