using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Shared.OpenAPI;

// https://learn.microsoft.com/en-us/azure/azure-functions/openapi-apim-integrate-visual-studio

public static class OpenApiConfigurationOptionsExtensions
{
    public static void AddOpenApi(this IServiceCollection services, string title)
    {
        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "1.0.0",
                    Title = title,
                    Description =
                        "This is a API for the Mad World projects.",
                    TermsOfService = new Uri("https://github.com/oveldman/MadWorldSuite/blob/main/README.md"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Oscar Veldman",
                        Email = "oveldman@gmail.com",
                        Url = new Uri("https://github.com/oveldman/MadWorldSuite/issues"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/oveldman/MadWorldSuite/blob/main/LICENSE"),
                    }
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = OpenApiVersionType.V2,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false,
            };

            return options;
        });
    }
}