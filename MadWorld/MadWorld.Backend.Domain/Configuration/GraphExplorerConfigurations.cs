namespace MadWorld.Backend.Domain.Configuration;

public sealed class GraphExplorerConfigurations
{
    public string ApplicationId { get; init; } = null!;
    public string TenantId { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string BaseUrl { get; init; } = null!;
}