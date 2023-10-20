using MadWorld.Backend.Domain.Configuration;

namespace MadWorld.Backend.Infrastructure.TableStorage;

public static class TableStorageConfigurationsManager
{
    public const int DefaultPageSize = 15;
    
    public static TableStorageConfigurations Get()
    {
        return new TableStorageConfigurations()
        {
            AzureWebJobsStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty
        };
    }
}