using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WireMock.Server;

namespace MadWorld.IntegrationTests.Startups;

public abstract class ApiStartupFactory : IAsyncDisposable
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

    protected virtual void PrepareHost()
    {
    }

    protected abstract IHost CreateHost();
}