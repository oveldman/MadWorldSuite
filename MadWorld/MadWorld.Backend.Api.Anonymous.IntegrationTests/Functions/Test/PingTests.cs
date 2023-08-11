using MadWorld.Backend.API.Anonymous.Functions.Test;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests.Functions.Test;

[Collection(CollectionTypes.IntegrationTests)]
public sealed class PingTests
{
    [Fact]
    public void Ping_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var context = Substitute.For<FunctionContext>();
        var request = Substitute.For<HttpRequestData>(context);

        // Act
        var response = Ping.Run(request, context);
        
        // Assert
        response.ShouldBe("Anonymous: Welcome to Azure Functions!");
    }
}