using MadWorld.Backend.Domain.Configuration;

namespace MadWorld.Backend.Infrastructure.BlobStorage;

public static class BlobStorageConfigurationsManager
{
    public static BlogStorageConfigurations Get()
    {
        return new BlogStorageConfigurations()
        {
            AzureWebJobsStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty
        };
    }
}