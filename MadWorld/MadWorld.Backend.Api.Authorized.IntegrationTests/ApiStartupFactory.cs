using MadWorld.Backend.API.Authorized.Extensions;
using MadWorld.Backend.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests;

public class ApiStartupFactory : IAsyncDisposable
{
    public IHost Host => _host ??= CreateHost();

    protected string AzureConnectionString = "UseDevelopmentStorage=true";

    private IHost? _host;

    public virtual ValueTask DisposeAsync()
    {
        var server = Host.Services.GetService<IWireMockServer>();
        server?.Stop();
        server?.Dispose();

        Host.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    protected virtual void PreRun()
    {
    }

    private IHost CreateHost()
    {
        PreRun();

        var wireMockServer = WireMockServer.Start();

        var configurationOverrider = new ConfigurationOverrider()
        {
            GraphExplorerBaseUrl = wireMockServer.Urls[0]
        };

        var host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                Environment.SetEnvironmentVariable("AzureWebJobsStorage", AzureConnectionString);
                Environment.SetEnvironmentVariable("AzureAD__ApplicationId", "ApplicationId");
                Environment.SetEnvironmentVariable("AzureAD__TenantId", "TenantId");
                Environment.SetEnvironmentVariable("AzureAD__ClientId", "ClientId");
                Environment.SetEnvironmentVariable("AzureAD__ClientSecret", "ClientSecret");
                builder.AddEnvironmentVariables();
            })
            .ConfigureServices(collection => { collection.AddSingleton(wireMockServer); })
            .BuildHost(configurationOverrider);
        
        return host;
    }
}