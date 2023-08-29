using MadWorld.IntegrationTests.Docker;

namespace MadWorld.Backend.JobRunner.IntegrationTests;

public class JobRunnerDockerStartupFactory : JobRunnerStartupFactory
{
    private DockerRunner _dockerRunner = null!;

    public override async ValueTask DisposeAsync()
    {
        await _dockerRunner.Stop();
        await base.DisposeAsync();
    }
    
    protected override void PrepareHost()
    {
        _dockerRunner = new DockerRunner(4);
        _dockerRunner.Start().GetAwaiter().GetResult();
        AzureConnectionString = _dockerRunner.GetConnectionString();
    }
}