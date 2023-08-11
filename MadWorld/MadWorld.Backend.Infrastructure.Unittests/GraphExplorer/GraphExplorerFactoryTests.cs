using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Infrastructure.Unittests.GraphExplorer;

public sealed class GraphExplorerFactoryTests
{
    [Fact]
    public void CreateClient_GivenConfiguration_ThenReturnNewClient()
    {
        // Arrange
        var configuration = new GraphExplorerConfigurations()
        {
            ApplicationId = "test-application-id",
            ClientId = "test-client-id",
            ClientSecret = "test-client-secret",
            TenantId = "test-tenant-id",
        };
        
        var loggerFactory = Substitute.For<ILoggerFactory>();
        var factory = new GraphExplorerFactory(loggerFactory);
        
        // Act
        var client = factory.CreateClient(configuration);

        // Assert
        client.ShouldNotBeNull();
    }
}