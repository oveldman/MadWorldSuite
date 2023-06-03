using MadWorld.Backend.API.Anonymous.Extensions;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

public sealed class ApiStartupFactory : IAsyncDisposable
{
    public readonly IHost Host;

    public ApiStartupFactory()
    {
        Host = new HostBuilder()
            .BuildHost();
    }

    public ValueTask DisposeAsync()
    {
        Host.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}