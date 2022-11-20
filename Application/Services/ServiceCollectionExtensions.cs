using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInnerServices(this IServiceCollection services)
    {
        services.AddScoped<IWordService, WordService>();

        return services;
    }
}