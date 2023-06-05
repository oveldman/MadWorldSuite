using Docker.DotNet;
using Docker.DotNet.Models;

namespace MadWorld.IntegrationTests.Docker;

public class DockerRunner
{
    private const string ImageId = "mcr.microsoft.com/azure-storage/azurite";
    private string _containerId = string.Empty;
    
    private readonly DockerClient _dockerClient;
    
    public DockerRunner()
    {
        _dockerClient = new DockerClientConfiguration()
            .CreateClient();
    }

    public async Task Start()
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

    public async Task Stop()
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