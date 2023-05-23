using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace MadWorld.Backend.Infrastructure.Unittests.GraphExplorer;

public class GraphExplorerFactoryTests
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
        
        var loggerFactory = new Mock<ILoggerFactory>();
        var factory = new GraphExplorerFactory(loggerFactory.Object);
        
        // Act
        var client = factory.CreateClient(configuration);

        // Assert
        client.ShouldNotBeNull();
    }
}