using System.Net;
using System.Text;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.API.Shared.Response;
using MadWorld.Backend.Api.Shared.Unittests._Mocks;
using MadWorld.Backend.Domain.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Shouldly;

namespace MadWorld.Backend.Api.Shared.Unittests.Response;

public sealed class ResponseMiddleWareTests
{
    private const string HttpTrigger = "HttpTrigger";
    
    [Fact]
    public async Task Invoke_WithNormalResponse_ShouldReturnSameObject()
    {
        // Arrange
        const string response = "test";
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData);
        var invocationResult = Substitute.For<InvocationResult>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(response);
    }
    
    [Fact]
    public async Task Invoke_WithNull_ShouldReturnSameObject()
    {
        // Arrange
        const string response = null!;
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData);
        var invocationResult = Substitute.For<InvocationResult>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(response);
    }
    
    [Fact]
    public async Task Invoke_WithOptionResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = Option<string>.Some(finalResponse);
        var metaData = GetMetaData();

        var invocationResult = Substitute.For<InvocationResult>();
        var context = FunctionContextMock.Create(metaData);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        invocationResult.Value.Returns(response);
        
        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(finalResponse);
    }
    
    [Fact]
    public async Task Invoke_WithResultOptionResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = new Result<Option<string>>(Option<string>.Some(finalResponse));
        var metaData = GetMetaData();

        var invocationResult = Substitute.For<InvocationResult>();
        var context = FunctionContextMock.Create(metaData);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(finalResponse);
    }
    
    [Fact]
    public async Task Invoke_WithResultResponse_ShouldReturnInnerResult()
    {
        // Arrange
        const string finalResponse = "test";
        var response = new Result<string>(finalResponse);
        var metaData = GetMetaData();

        var invocationResult = Substitute.For<InvocationResult>();
        var context = FunctionContextMock.Create(metaData);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(finalResponse);
    }
    
    [Fact]
    public async Task Invoke_WithArgumentExceptionResponse_ShouldReturnErrorResponse500()
    {
        // Arrange
        var exception = new ArgumentException("Test");
        var response = new Result<string>(exception);

        var invocationResult = Substitute.For<InvocationResult>();
        var context = FunctionContextMock.Create(GetMetaData());
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var stream = Substitute.For<Stream>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var httpResponseData = Substitute.For<HttpResponseData>(context); 
        ;
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        httpRequestData
            .CreateResponse()
            .Returns(httpResponseData);
        httpResponseData.Body.Returns(stream);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(httpResponseData);
        httpResponseData.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);

        var bytes = "{\"Message\":\"There went something wrong. Please try again later.\"}"u8.ToArray();
        await stream.Received(1).WriteAsync(
            Arg.Is<byte[]>(buffer => buffer.SequenceEqual(bytes)),
            Arg.Is<int>(offset => offset == 0),
            Arg.Is<int>(length => length == bytes.Length),
            Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Invoke_WithValidationExceptionResponse_ShouldReturnErrorResponse400()
    {
        // Arrange
        const string errorMessage = "test";
        var exception = new ValidationException(errorMessage);
        var response = new Result<string>(exception);
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData);
        var invocationResult = Substitute.For<InvocationResult>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var stream = Substitute.For<Stream>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var httpResponseData = Substitute.For<HttpResponseData>(context);
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        httpRequestData
            .CreateResponse()
            .Returns(httpResponseData);
        httpResponseData.Body.Returns(stream);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(httpResponseData);
        httpResponseData.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        const string errorResponse = "{\"Message\":\"" + errorMessage + "\"}";
        var bytes = Encoding.UTF8.GetBytes(errorResponse);
        await stream.Received(1).WriteAsync(
            Arg.Is<byte[]>(buffer => buffer.SequenceEqual(bytes)),
            Arg.Is<int>(offset => offset == 0),
            Arg.Is<int>(length => length == bytes.Length),
            Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Invoke_WithOptionNoneResponse_ShouldReturnErrorResponse404()
    {
        // Arrange
        var response = Option<string>.None;
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData);
        var invocationResult = Substitute.For<InvocationResult>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var stream = Substitute.For<Stream>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var httpResponseData = Substitute.For<HttpResponseData>(context);
        
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        httpRequestData
            .CreateResponse()
            .Returns(httpResponseData);
        httpResponseData.Body.Returns(stream);
        invocationResult.Value.Returns(response);

        var middleware = new ResponseMiddleWare(contextWrapper);

        // Act
        await middleware.Invoke(context, _ => Task.CompletedTask);

        // Assert
        invocationResult.Value.ShouldBe(httpResponseData);
        httpResponseData.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        var bytes = "{\"Message\":\"Not found\"}"u8.ToArray();
        await stream.Received(1).WriteAsync(
            Arg.Is<byte[]>(buffer => buffer.SequenceEqual(bytes)),
            Arg.Is<int>(offset => offset == 0),
            Arg.Is<int>(length => length == bytes.Length),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Invoke_WithBlobTrigger_ShouldContinue()
    {
        // Arrange
        const string trigger = "blobsTrigger";
        var metaData = GetMetaData(trigger);

        var context = FunctionContextMock.Create(metaData);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var next = Substitute.For<FunctionExecutionDelegate>();
        
        var middleware = new ResponseMiddleWare(contextWrapper);
        
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        await next.Received(1).Invoke(Arg.Any<FunctionContext>());
    }

    private static IEnumerable<BindingMetadata> GetMetaData(string triggerType = HttpTrigger)
    {
        var metaData = Substitute.For<BindingMetadata>();
        metaData.Type.Returns(triggerType);

        return new List<BindingMetadata>()
        {
            metaData
        };
    }
}