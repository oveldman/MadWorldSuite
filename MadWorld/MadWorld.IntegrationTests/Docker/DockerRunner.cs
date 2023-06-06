using Docker.DotNet;
using Docker.DotNet.Models;

namespace MadWorld.IntegrationTests.Docker;

public class DockerRunner
{
    private const string ImageId = "mcr.microsoft.com/azure-storage/azurite";
    private const string AzuriteAccountName = "devstoreaccount1";
    private const string AzuriteAccountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
    private readonly int _azuritePortRange = 0;
    private string _containerId = string.Empty;
    
    private readonly DockerClient _dockerClient;
    
    public DockerRunner(int azuritePortRange)
    {
        _azuritePortRange = azuritePortRange;
        
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
                AutoRemove = true,
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
                WaitBeforeKillSeconds = 0
            },
            CancellationToken.None);
    }

    public string GetConnectionString()
    {
        return $"DefaultEndpointsProtocol=http;AccountName={AzuriteAccountName};"
            + $"AccountKey={AzuriteAccountKey};"
            + $"BlobEndpoint=http://127.0.0.1:100{_azuritePortRange}0/{AzuriteAccountName};"
            + $"QueueEndpoint=http://127.0.0.1:100{_azuritePortRange}1/{AzuriteAccountName};"
            + $"TableEndpoint=http://127.0.0.1:100{_azuritePortRange}2/{AzuriteAccountName};";
    }
    
    private Dictionary<string, IList<PortBinding>> GetAzuritePorts()
    {
        return new Dictionary<string, IList<PortBinding>>
        {
            {
                "10000/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = $"100{_azuritePortRange}0"
                    }
                }
            },
            {
                $"10001/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = $"100{_azuritePortRange}1"
                    }
                }
            },
            {
                $"10002/tcp",
                new List<PortBinding>
                {
                    new()
                    {
                        HostPort = $"100{_azuritePortRange}2"
                    }
                }
            },
        };
    }
}