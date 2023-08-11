using System.Collections.Immutable;
using System.Net;
using System.Security.Claims;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.API.Shared.Functions.Status;
using MadWorld.Backend.Api.Shared.Unittests._Mocks;
using MadWorld.Backend.Api.Shared.Unittests.Authorization.TestUtils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.Api.Shared.Unittests.Authorization;

public sealed class AuthorizeMiddleWareTests
{
    private const string HttpTrigger = "HttpTrigger";
    
    // Has only role: None & User
    private const string Headers = @"{
                            'Authorization':'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHRlbnNpb25fUm9sZXMiOiJub25lO3VzZXIifQ.v6qH7gZb2D-NTicpqYDTnn2-33N_WttBiM-Q0DPKvW8'
                        }";
    
    private const string AnonymousHeaders = @"{}";
    
    [Fact]
    public async Task Invoke_GivenBearerRequest_BearerParsedSuccessfully()
    {
        // Arrange
        const string functionTypeName = $"MadWorld.Backend.API.Shared.Functions.Status{nameof(GetStatus)}.Run";
        
        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.API.Shared.dll");
        var metaData = GetMetaData();

        var context = FunctionContextMock.Create(metaData, assemblyLocation, string.Empty, functionTypeName, Headers);

        var next = Substitute.For<FunctionExecutionDelegate>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        await next.Received(1).Invoke(Arg.Any<FunctionContext>());
        context.Features.Received(1).Set(Arg.Any<ClaimsPrincipal>());
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequestAndIsAnonymousEndpoint_ThenExecutedSuccessfully()
    {
        // Arrange
        const string functionName = nameof(GetStatus);
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData, string.Empty, functionName, string.Empty, AnonymousHeaders);
        var next = Substitute.For<FunctionExecutionDelegate>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        await next.Received(1).Invoke(Arg.Any<FunctionContext>());
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequestAndIsLocalhostEndpoint_ThenExecutedSuccessfully()
    {
        // Arrange
        const string functionName = nameof(MockFunction);

        var url = new Uri($"https://localhost:7071/api/{functionName}");
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData, string.Empty, functionName, string.Empty, AnonymousHeaders);
        var next = Substitute.For<FunctionExecutionDelegate>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        httpRequestData.Url.Returns(url);

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        await next.Received(1).Invoke(Arg.Any<FunctionContext>());
    }
    
    [Fact]
    public async Task Invoke_GivenUserHasNotRole_ReturnUnauthorized()
    {
        // Arrange
        const string functionTypeName = $"MadWorld.Backend.Api.Shared.Unittests.Authorization.TestUtils.{nameof(MockFunction)}.Run";

        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.Api.Shared.Unittests.dll");
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData, assemblyLocation, string.Empty, functionTypeName, Headers);

        var next = Substitute.For<FunctionExecutionDelegate>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var httpResponseData = Substitute.For<HttpResponseData>(context);
        var invocationResult = Substitute.For<InvocationResult>();
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var stream = Substitute.For<Stream>();

        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        contextWrapper
            .GetInvocationResult(Arg.Any<FunctionContext>())
            .Returns(invocationResult);
        httpRequestData.CreateResponse().Returns(httpResponseData);
        httpResponseData.Body.Returns(stream);

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        invocationResult.Received(1).Value = httpResponseData;
        httpResponseData.Received(1).StatusCode = HttpStatusCode.Unauthorized;

        var bytes = "401 - Unauthorized!!!"u8.ToArray();
        await stream.Received(1).WriteAsync(
            Arg.Is<byte[]>(buffer => buffer.SequenceEqual(bytes)),
            Arg.Is<int>(offset => offset == 0),
            Arg.Is<int>(length => length == bytes.Length),
            Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequest_ReturnUnauthorized()
    {
        // Arrange
        const string functionTypeName = $"MadWorld.Backend.Api.Shared.Unittests.Authorization.Mocks.{nameof(MockFunction)}.Run";
        var url = new Uri($"https://www.test.nl/api/{nameof(MockFunction)}");
        
        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.Api.Shared.Unittests.dll");
        var metaData = GetMetaData();
        
        var context = FunctionContextMock.Create(metaData, assemblyLocation, string.Empty, functionTypeName, AnonymousHeaders);
        var next = Substitute.For<FunctionExecutionDelegate>();
        var invocationResult = Substitute.For<InvocationResult>();
        var stream = Substitute.For<Stream>();
        var httpRequestData = Substitute.For<HttpRequestData>(context);
        var httpResponseData = Substitute.For<HttpResponseData>(context);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        
        contextWrapper
            .GetHttpRequestDataAsync(Arg.Any<FunctionContext>())
            .Returns(httpRequestData);
        contextWrapper.GetInvocationResult(Arg.Any<FunctionContext>()).Returns(invocationResult);
        httpRequestData.CreateResponse().Returns(httpResponseData);
        httpRequestData.Url.Returns(url);
        httpResponseData.Body.Returns(stream);

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        // Act
        await middleware.Invoke(context, next);
        
        // Assert
        invocationResult.Received(1).Value = httpResponseData;
        httpResponseData.Received(1).StatusCode = HttpStatusCode.Unauthorized;

        var bytes = "401 - Unauthorized!!!"u8.ToArray();
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
        const string trigger = "blobTrigger";
        
        var metaData = GetMetaData(trigger);
        
        var context = FunctionContextMock.Create(metaData);
        var contextWrapper = Substitute.For<IFunctionContextWrapper>();
        var next = Substitute.For<FunctionExecutionDelegate>();

        var middleware = new AuthorizeMiddleWare(contextWrapper);
        
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