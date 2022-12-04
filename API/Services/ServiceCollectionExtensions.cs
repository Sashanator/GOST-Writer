using Gost.Services.GostService;
using Gost.Services.WordService;

namespace Gost.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInnerServices(this IServiceCollection services)
    {
        services.AddScoped<IWordService, WordService.WordService>();
        services.AddScoped<IGostService, GostService.GostService>();

        return services;
    }
}