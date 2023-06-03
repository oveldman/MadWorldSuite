using MadWorld.Backend.API.Anonymous.Functions.Test;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Shouldly;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests.Functions;

public sealed class PingTests
{
    [Fact]
    public void Ping_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var context = new Mock<FunctionContext>();
        var request = new Mock<HttpRequestData>(context.Object);

        // Act
        var response = Ping.Run(request.Object, context.Object);
        
        // Assert
        response.ShouldBe("Anonymous: Welcome to Azure Functions!");
    }
}