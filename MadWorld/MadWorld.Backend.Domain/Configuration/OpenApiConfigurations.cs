namespace MadWorld.Backend.Domain.Configuration;

public sealed class OpenApiConfigurations
{
    public string BaseUrl { get; init; } = string.Empty;
    public string GitUrl { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
}