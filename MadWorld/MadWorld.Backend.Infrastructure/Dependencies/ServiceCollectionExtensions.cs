using Azure.Data.Tables;
using Azure.Storage.Blobs;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.Storage;
using MadWorld.Backend.Infrastructure.BlobStorage;
using MadWorld.Backend.Infrastructure.BlobStorage.Blog;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using MadWorld.Backend.Infrastructure.TableStorage;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Infrastructure.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationOverrider? configurationOverrider = null)
    {
        services.AddBlobStorage();
        services.AddGraphExplorer(configurationOverrider);
        services.AddTableStorage();

        return services;
    }
    
    private static void AddBlobStorage(this IServiceCollection services)
    {
        var configuration = BlobStorageConfigurationsManager.Get();
        
        services.AddSingleton<BlobServiceClient>(s => new BlobServiceClient(configuration.AzureWebJobsStorage));
        
        services.AddScoped<IStorageClient, BlobStorageClient>();
        services.AddScoped<IBlogStorageClient, BlogStorageClient>();
    }
    
    private static void AddGraphExplorer(this IServiceCollection services, ConfigurationOverrider? configurationOverrider)
    {
        var configuration = GraphExplorerConfigurationsManager.Get(configurationOverrider);
        services.AddSingleton<GraphExplorerFactory>();
        
        services.AddScoped<IGraphExplorerClient>(s => s.GetRequiredService<GraphExplorerFactory>().CreateClient(configuration));
    }
    
    private static void AddTableStorage(this IServiceCollection services)
    {
        var configuration = TableStorageConfigurationsManager.Get();
        
        services.AddSingleton<TableServiceClient>(s => new TableServiceClient(configuration.AzureWebJobsStorage));
        
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ICurriculumVitaeRepository, CurriculumVitaeRepository>();
    }
}