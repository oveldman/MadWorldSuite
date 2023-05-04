using MadWorld.Backend.Domain.Configuration;

namespace MadWorld.Backend.API.Shared.OpenAPI;

public static class OpenApiConfigurationManager
{
    public static OpenApiConfigurations Get()
    {
        return new OpenApiConfigurations()
        {
            BaseUrl = Environment.GetEnvironmentVariable("OpenApi__BaseUrl") ?? string.Empty,
            GitUrl = Environment.GetEnvironmentVariable("OpenApi__GitUrl") ?? string.Empty,
            Title = Environment.GetEnvironmentVariable("OpenApi__DocumentName") ?? string.Empty
        };
    }
}