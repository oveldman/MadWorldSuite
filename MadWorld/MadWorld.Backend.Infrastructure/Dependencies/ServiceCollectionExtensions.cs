using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Infrastructure.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddGraphExplorer();

        return services;
    }
    
    private static void AddGraphExplorer(this IServiceCollection services)
    {
        var configuration = GraphExplorerConfigurationsManager.Get();
        
        services.AddScoped<IGraphExplorerClient>(_ => new GraphExplorerFactory().CreateClient(configuration));
    }
}