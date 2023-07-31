using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.ExternPackages.Monaco.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMonaco(this IServiceCollection services)
    {
        services.AddTransient<MonacoManager>();

        return services;
    }
}