using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Infrastructure.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationOverrider? configurationOverrider = null)
    {
        services.AddGraphExplorer(configurationOverrider);

        return services;
    }
    
    private static void AddGraphExplorer(this IServiceCollection services, ConfigurationOverrider? configurationOverrider)
    {
        var configuration = GraphExplorerConfigurationsManager.Get(configurationOverrider);
        services.AddSingleton<GraphExplorerFactory>();
        
        services.AddScoped<IGraphExplorerClient>(s => s.GetRequiredService<GraphExplorerFactory>().CreateClient(configuration));
    }
}