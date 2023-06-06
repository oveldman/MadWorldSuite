using Docker.DotNet;
using Docker.DotNet.Models;
using JetBrains.Annotations;
using MadWorld.IntegrationTests.Docker;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

[UsedImplicitly]
public sealed class AnonymousApiDockerStartupFactory : AnonymousApiStartupFactory
{
    private DockerRunner _dockerRunner = null!;

    public override async ValueTask DisposeAsync()
    {
        await _dockerRunner.Stop();
        await base.DisposeAsync();
    }
    
    protected override void PrepareHost()
    {
        _dockerRunner = new DockerRunner(1);
        _dockerRunner.Start().GetAwaiter().GetResult();
        AzureConnectionString = _dockerRunner.GetConnectionString();
    }
}