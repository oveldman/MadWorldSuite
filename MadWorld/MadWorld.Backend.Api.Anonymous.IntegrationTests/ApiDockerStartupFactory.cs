using Docker.DotNet;
using Docker.DotNet.Models;
using JetBrains.Annotations;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests;

[UsedImplicitly]
public sealed class ApiDockerStartupFactory : ApiStartupFactory
{
    private const string ImageId = "mcr.microsoft.com/azure-storage/azurite";
    private string _containerId = string.Empty;
    
    private readonly DockerClient _dockerClient;

    public ApiDockerStartupFactory() : base()
    {
        _dockerClient = new DockerClientConfiguration()
            .CreateClient();

        StartAzuriteDocker().GetAwaiter().GetResult();
    }

    public override async ValueTask DisposeAsync()
    {
        await StopAzuriteDocker();
        await base.DisposeAsync();
    }

    private async Task StartAzuriteDocker()
    {
        await _dockerClient.Images.CreateImageAsync(
            new ImagesCreateParameters()
            {
                FromImage = ImageId,
                Tag = "latest"
            }, 
            null, 
            new Progress<JSONMessage>());
        
        var containerResponse = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = ImageId,
            HostConfig = new HostConfig()
            {
                PortBindings = GetAzuritePorts()
            }
        });
        
        _containerId = containerResponse.ID;
        
        await _dockerClient.Containers.StartContainerAsync(
            _containerId,
            new ContainerStartParameters()
        );
    }

    private async Task StopAzuriteDocker()
    {
        await _dockerClient.Containers.StopContainerAsync(
            _containerId,
            new ContainerStopParameters
            {
                WaitBeforeKillSeconds = 30
            },
            CancellationToken.None);
        
        await _dockerClient.Containers.RemoveContainerAsync(
            _containerId,
            new ContainerRemoveParameters(),
            CancellationToken.None);
    }

    private static Dictionary<string, IList<PortBinding>> GetAzuritePorts()
    {
        return new Dictionary<string, IList<PortBinding>>
        {
            {
                "10000/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = "10000"
                    }
                }
            },
            {
                "10001/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = "10001"
                    }
                }
            },
            {
                "10002/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = "10002"
                    }
                }
            },
        };
    }
}