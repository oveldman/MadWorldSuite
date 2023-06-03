using System.Net;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.API.Shared.Response;
using MadWorld.Backend.Domain.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.Api.Shared.Unittests.Response;

public sealed class ResponseMiddleWareTests
{
    private const string HttpTrigger = "HttpTrigger";
    
    [Fact]
    public async Task Invoke_WithNormalResponse_ShouldReturnSameObject()
    {
        // Arrange
        const string response = "test";

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        invocationResult.Setup(i => i.Value).Returns(response);
        var middleware = new ResponseMiddleWare(contextWrapper.Object);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = It.IsAny<object>(), Times.Never());
    }
    
    [Fact]
    public async Task Invoke_WithNull_ShouldReturnSameObject()
    {
        // Arrange
        const string response = null!;

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
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
    public async Task Invoke_WithOptionResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = Option<string>.Some(finalResponse);

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
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
    
    [Fact]
    public async Task Invoke_WithResultOptionResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = new Result<Option<string>>(Option<string>.Some(finalResponse));

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
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
    
    [Fact]
    public async Task Invoke_WithResultResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = new Result<string>(finalResponse);

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
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
    
    [Fact]
    public async Task Invoke_WithArgumentExceptionResponse_ShouldReturnErrorResponse500()
    {
        // Arrange
        var exception = new ArgumentException("Test");
        var response = new Result<string>(exception);

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var stream = new Mock<Stream>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var httpResponseData = new Mock<HttpResponseData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        contextWrapper
            .Setup(c =>
                c.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        httpRequestData
            .Setup(hrd =>
                hrd.CreateResponse())
            .Returns(httpResponseData.Object);
        httpResponseData.Setup(hrd => hrd.Body).Returns(stream.Object);

        var middleware = new ResponseMiddleWare(contextWrapper.Object);
        invocationResult.Setup(i => i.Value).Returns(response);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = httpResponseData.Object, Times.Once);
        httpResponseData.VerifySet(x => x.StatusCode = HttpStatusCode.InternalServerError, Times.Once);

        var bytes = "{\"Message\":\"There went something wrong. Please try again later.\"}"u8.ToArray();
        stream.Verify(x => x.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task Invoke_WithValidationExceptionResponse_ShouldReturnErrorResponse400()
    {
        // Arrange
        const string errorMessage = "test";
        var exception = new ValidationException(errorMessage);
        var response = new Result<string>(exception);

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var stream = new Mock<Stream>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var httpResponseData = new Mock<HttpResponseData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        contextWrapper
            .Setup(c =>
                c.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        httpRequestData
            .Setup(hrd =>
                hrd.CreateResponse())
            .Returns(httpResponseData.Object);
        httpResponseData.Setup(hrd => hrd.Body).Returns(stream.Object);

        var middleware = new ResponseMiddleWare(contextWrapper.Object);
        invocationResult.Setup(i => i.Value).Returns(response);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = httpResponseData.Object, Times.Once);
        httpResponseData.VerifySet(x => x.StatusCode = HttpStatusCode.BadRequest, Times.Once);

        const string errorResponse = "{\"Message\":\"" + errorMessage + "\"}";
        var bytes = Encoding.UTF8.GetBytes(errorResponse);
        stream.Verify(x => x.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task Invoke_WithOptionNoneResponse_ShouldReturnErrorResponse404()
    {
        // Arrange
        var response = Option<string>.None;

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var stream = new Mock<Stream>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var httpResponseData = new Mock<HttpResponseData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        contextWrapper
            .Setup(c =>
                c.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        httpRequestData
            .Setup(hrd =>
                hrd.CreateResponse())
            .Returns(httpResponseData.Object);
        httpResponseData.Setup(hrd => hrd.Body).Returns(stream.Object);
        invocationResult.Setup(i => i.Value).Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper.Object);

        // Act
        await middleware.Invoke(context.Object, _ => Task.CompletedTask);

        // Assert
        invocationResult.VerifySet(x => x.Value = httpResponseData.Object, Times.Once);
        httpResponseData.VerifySet(x => x.StatusCode = HttpStatusCode.NotFound, Times.Once);

        var bytes = "{\"Message\":\"Not found\"}"u8.ToArray();
        stream.Verify(x => x.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Invoke_WithBlobTrigger_ShouldContinue()
    {
        // Arrange
        const string trigger = "blobsTrigger";
        
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        var next = new Mock<FunctionExecutionDelegate>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData(trigger));
        
        var middleware = new ResponseMiddleWare(contextWrapper.Object);
        
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        next.Verify(n => n.Invoke(It.IsAny<FunctionContext>()), Times.Once());
    }

    private static IEnumerable<BindingMetadata> GetMetaData(string triggerType = HttpTrigger)
    {
        var metaData = new Mock<BindingMetadata>();
        metaData.Setup(md => md.Type).Returns(triggerType);

        return new List<BindingMetadata>()
        {
            metaData.Object
        };
    }
}