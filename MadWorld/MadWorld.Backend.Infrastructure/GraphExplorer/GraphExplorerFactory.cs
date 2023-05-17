using Azure.Identity;
using MadWorld.Backend.Domain.Configuration;
using Microsoft.Graph;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

internal class GraphExplorerFactory
{
    private readonly string[] _scopes;

    internal GraphExplorerFactory()
    {
        _scopes = new[] { "https://graph.microsoft.com/.default" };
    }

    internal GraphExplorerClient CreateClient(GraphExplorerConfigurations configurations)
    {
        var graphServiceClient = CreateGraphServiceClient(configurations);
        
        return new GraphExplorerClient(graphServiceClient, configurations);
    }
    
    private GraphServiceClient CreateGraphServiceClient(GraphExplorerConfigurations configurations)
    {
        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var clientSecretCredential = new ClientSecretCredential(
            configurations.TenantId, configurations.ClientId, configurations.ClientSecret, options);
        
        return new GraphServiceClient(clientSecretCredential, _scopes);
    }
}