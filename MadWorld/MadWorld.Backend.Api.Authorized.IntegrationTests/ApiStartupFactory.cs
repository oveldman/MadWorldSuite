using MadWorld.Backend.API.Authorized.Extensions;
using MadWorld.Backend.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph.Models.ExternalConnectors;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests;

public class ApiStartupFactory : IAsyncDisposable
{
    public readonly IHost Host;

    public ApiStartupFactory()
    {
        var wireMockServer = WireMockServer.Start();
        
        var configurationOverrider = new ConfigurationOverrider()
        {
            GraphExplorerBaseUrl = wireMockServer.Urls[0]
        };

        Host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                Environment.SetEnvironmentVariable("AzureAD__ApplicationId", "ApplicationId");
                Environment.SetEnvironmentVariable("AzureAD__TenantId", "TenantId");
                Environment.SetEnvironmentVariable("AzureAD__ClientId", "ClientId");
                Environment.SetEnvironmentVariable("AzureAD__ClientSecret", "ClientSecret");
                builder.AddEnvironmentVariables();
            })
            .ConfigureServices(collection =>
            {
                collection.AddSingleton(wireMockServer);
            })
            .BuildHost(configurationOverrider);
    }

    public ValueTask DisposeAsync()
    {
        var server = Host.Services.GetService<IWireMockServer>();
        server?.Stop();
        server?.Dispose();
        
        Host.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}