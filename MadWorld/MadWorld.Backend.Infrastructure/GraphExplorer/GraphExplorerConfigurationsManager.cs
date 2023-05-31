using MadWorld.Backend.Domain.Configuration;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

internal static class GraphExplorerConfigurationsManager
{
    internal static GraphExplorerConfigurations Get()
    {
        return new GraphExplorerConfigurations()
        {
            ApplicationId = Environment.GetEnvironmentVariable("AzureAD__ApplicationId") ?? string.Empty,
            TenantId = Environment.GetEnvironmentVariable("AzureAD__TenantId") ?? string.Empty,
            ClientId = Environment.GetEnvironmentVariable("AzureAD__ClientId") ?? string.Empty,
            ClientSecret = Environment.GetEnvironmentVariable("AzureAD__ClientSecret") ?? string.Empty,
            BaseUrl = Environment.GetEnvironmentVariable("GraphExplorer__BaseUrl")!
        };
    }
}