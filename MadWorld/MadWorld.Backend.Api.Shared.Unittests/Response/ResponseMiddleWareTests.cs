using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.API.Shared.Response;
using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.Api.Shared.Unittests.Response;

public class ResponseMiddleWareTests
{
    [Fact]
    public async Task Invoke_WithNormalResponse_ShouldReturnSameObject()
    {
        // Arrange
        const string response = "test";

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        
        var middleware = new ResponseMiddleWare(contextWrapper.Object);
        invocationResult.Setup(i => i.Value).Returns(response);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = It.IsAny<object>(), Times.Never());
    }
    
    [Fact]
    public async Task Invoke_WithResultResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = new Result<string>(finalResponse);

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        
        var middleware = new ResponseMiddleWare(contextWrapper.Object);
        invocationResult.Setup(i => i.Value).Returns(response);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = finalResponse, Times.Once);
    }
}