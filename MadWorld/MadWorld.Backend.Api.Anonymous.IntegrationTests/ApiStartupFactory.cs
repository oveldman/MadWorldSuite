using MadWorld.Backend.API.Anonymous.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

public class ApiStartupFactory : IAsyncDisposable
{
    public readonly IHost Host;

    public ApiStartupFactory()
    {
        Host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                Environment.SetEnvironmentVariable("AzureWebJobsStorage", "UseDevelopmentStorage=true");
                Environment.SetEnvironmentVariable("AzureAD__ApplicationId", "ApplicationId");
                Environment.SetEnvironmentVariable("AzureAD__TenantId", "TenantId");
                Environment.SetEnvironmentVariable("AzureAD__ClientId", "ClientId");
                Environment.SetEnvironmentVariable("AzureAD__ClientSecret", "ClientSecret");
                builder.AddEnvironmentVariables();
            })
            .BuildHost();
    }

    public virtual ValueTask DisposeAsync()
    {
        Host.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}