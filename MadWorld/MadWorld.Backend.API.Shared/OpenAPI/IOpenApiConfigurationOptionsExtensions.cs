using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Shared.OpenAPI;

// https://learn.microsoft.com/en-us/azure/azure-functions/openapi-apim-integrate-visual-studio

public static class OpenApiConfigurationOptionsExtensions
{
    public static void AddOpenApi(this IServiceCollection services, bool isDevelopment)
    {
        var configuration = OpenApiConfigurationManager.Get();
        
        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "1.0.0",
                    Title = configuration.Title,
                    Description =
                        "This is a API for the Mad World projects.",
                    TermsOfService = new Uri($"{configuration.GitUrl}/MadWorldSuite/blob/main/README.md"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Oscar Veldman",
                        Email = "oveldman@gmail.com",
                        Url = new Uri($"{configuration.GitUrl}/MadWorldSuite/issues"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri($"{configuration.GitUrl}/MadWorldSuite/blob/main/LICENSE"),
                    }
                },
                Servers = new List<OpenApiServer>()
                {
                    new()
                    {
                        Url = configuration.BaseUrl
                    }
                },
                OpenApiVersion = OpenApiVersionType.V3,
                IncludeRequestingHostName = isDevelopment,
                ForceHttps = false,
                ForceHttp = false
            };

            return options;
        });
    }
}