using MadWorld.Backend.Domain.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

public sealed class GraphExplorerHealthCheck : IHealthCheck
{
    private readonly IGraphExplorerClient _client;

    public GraphExplorerHealthCheck(IGraphExplorerClient client)
    {
        _client = client;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _client.TestConnection();
        return result.Match(
            r => HealthCheckResult.Healthy(),
            exception => HealthCheckResult.Unhealthy(
                description: "Graph Explorer failed to get profile from Graph API.",
                exception: exception));
    }
}