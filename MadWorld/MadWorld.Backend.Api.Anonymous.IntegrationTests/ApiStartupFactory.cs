using MadWorld.Backend.API.Anonymous.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

public class ApiStartupFactory : IAsyncDisposable
{
    public IHost Host => _host ??= CreateHost();

    protected string AzureConnectionString = "UseDevelopmentStorage=true";

    private IHost? _host;

    public virtual ValueTask DisposeAsync()
    {
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
            .BuildHost();

        return host;
    }
}