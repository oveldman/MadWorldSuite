using JetBrains.Annotations;
using MadWorld.IntegrationTests.Docker;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests;

[UsedImplicitly]
public sealed class ApiDockerStartupFactory : ApiStartupFactory
{
    private DockerRunner _dockerRunner = null!;

    public override async ValueTask DisposeAsync()
    {
        await _dockerRunner.Stop();
        await base.DisposeAsync();
    }
    
    protected override void PreRun()
    {
        _dockerRunner = new DockerRunner(2);
        _dockerRunner.Start().GetAwaiter().GetResult();
        AzureConnectionString = _dockerRunner.GetConnectionString();
    }
}