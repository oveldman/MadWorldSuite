using Azure.Identity;
using MadWorld.Backend.Domain.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

internal class GraphExplorerFactory
{
    private readonly string[] _scopes;
    private readonly ILoggerFactory _loggerFactory;

    public GraphExplorerFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _scopes = new[] { "https://graph.microsoft.com/.default" };
    }

    public GraphExplorerClient CreateClient(GraphExplorerConfigurations configurations)
    {
        var graphServiceClient = CreateGraphServiceClient(configurations);
        
        return new GraphExplorerClient(graphServiceClient, configurations, _loggerFactory.CreateLogger<GraphExplorerClient>());
    }
    
    private GraphServiceClient CreateGraphServiceClient(GraphExplorerConfigurations configurations)
    {
        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var clientSecretCredential = new ClientSecretCredential(
            configurations.TenantId, configurations.ClientId, configurations.ClientSecret, options);
        
        return new GraphServiceClient(clientSecretCredential, _scopes, configurations.BaseUrl);
    }
}