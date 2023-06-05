using Docker.DotNet;
using Docker.DotNet.Models;
using JetBrains.Annotations;
using MadWorld.IntegrationTests.Docker;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

[UsedImplicitly]
public sealed class ApiDockerStartupFactory : ApiStartupFactory
{
    private readonly DockerRunner _dockerRunner;

    public ApiDockerStartupFactory() : base()
    {
        _dockerRunner = new DockerRunner();
        _dockerRunner.Start().GetAwaiter().GetResult();
    }

    public override async ValueTask DisposeAsync()
    {
        await _dockerRunner.Stop();
        await base.DisposeAsync();
    }
}