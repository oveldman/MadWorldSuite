using System.Net;
using System.Security.Claims;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.API.Shared.Functions.Status;
using MadWorld.Backend.Api.Shared.Unittests.Authorization.TestUtils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.Api.Shared.Unittests.Authorization;

public class AuthorizeMiddleWareTests
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
        
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var features = new Mock<IInvocationFeatures>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(Headers);
        context.Setup(c => c.Features).Returns(features.Object);
        context.Setup(c => c.FunctionDefinition.EntryPoint).Returns(functionTypeName);
        context.Setup(c => c.FunctionDefinition.PathToAssembly).Returns(assemblyLocation);

        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        next.Verify(n => n.Invoke(It.IsAny<FunctionContext>()), Times.Once());
        context.Verify(c => c.Features.Set(It.IsAny<ClaimsPrincipal>()), Times.Once());
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequestAndIsAnonymousEndpoint_ThenExecutedSuccessfully()
    {
        // Arrange
        const string functionName = nameof(GetStatus);
        
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(AnonymousHeaders);
        context.Setup(c => c.FunctionDefinition.Name).Returns(functionName);

        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        next.Verify(n => n.Invoke(It.IsAny<FunctionContext>()), Times.Once());
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequestAndIsLocalhostEndpoint_ThenExecutedSuccessfully()
    {
        // Arrange
        const string functionName = nameof(MockFunction);

        var url = new Uri($"https://localhost:7071/api/{functionName}");
        
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(AnonymousHeaders);
        context.Setup(c => c.FunctionDefinition.Name).Returns(functionName);
        contextWrapper
            .Setup(cw => cw.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        httpRequestData.Setup(hrd => hrd.Url).Returns(url);
        
        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        next.Verify(n => n.Invoke(It.IsAny<FunctionContext>()), Times.Once());
    }
    
    [Fact]
    public async Task Invoke_GivenUserHasNotRole_ReturnUnauthorized()
    {
        // Arrange
        const string functionTypeName = $"MadWorld.Backend.Api.Shared.Unittests.Authorization.TestUtils.{nameof(MockFunction)}.Run";
        var url = new Uri($"https://www.test.nl/api/{nameof(MockFunction)}");
        
        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.Api.Shared.Unittests.dll");

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var features = new Mock<IInvocationFeatures>();
        var stream = new Mock<Stream>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var httpResponseData = new Mock<HttpResponseData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(Headers);
        context.Setup(c => c.Features).Returns(features.Object);
        context.Setup(c => c.FunctionDefinition.EntryPoint).Returns(functionTypeName);
        context.Setup(c => c.FunctionDefinition.PathToAssembly).Returns(assemblyLocation);
        contextWrapper
            .Setup(cw => cw.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        httpRequestData
            .Setup(hrd => hrd.CreateResponse())
            .Returns(httpResponseData.Object);
        httpRequestData.Setup(hrd => hrd.Url).Returns(url);
        httpResponseData.Setup(hrd => hrd.Body).Returns(stream.Object);

        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        invocationResult.VerifySet(x => x.Value = httpResponseData.Object, Times.Once);
        httpResponseData.VerifySet(x => x.StatusCode = HttpStatusCode.Unauthorized, Times.Once);

        var bytes = "401 - Unauthorized!!!"u8.ToArray();
        stream.Verify(x => x.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task Invoke_GivenWithoutBearerRequest_ReturnUnauthorized()
    {
        // Arrange
        const string functionTypeName = $"MadWorld.Backend.Api.Shared.Unittests.Authorization.Mocks.{nameof(MockFunction)}.Run";
        var url = new Uri($"https://www.test.nl/api/{nameof(MockFunction)}");
        
        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.Api.Shared.Unittests.dll");

        var invocationResult = new Mock<InvocationResult>();
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var features = new Mock<IInvocationFeatures>();
        var stream = new Mock<Stream>();
        var httpRequestData = new Mock<HttpRequestData>(context.Object);
        var httpResponseData = new Mock<HttpResponseData>(context.Object);
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData());
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(AnonymousHeaders);
        context.Setup(c => c.Features).Returns(features.Object);
        context.Setup(c => c.FunctionDefinition.EntryPoint).Returns(functionTypeName);
        context.Setup(c => c.FunctionDefinition.PathToAssembly).Returns(assemblyLocation);
        contextWrapper
            .Setup(cw => cw.GetHttpRequestDataAsync(It.IsAny<FunctionContext>()))
            .ReturnsAsync(httpRequestData.Object);
        contextWrapper
            .Setup(c =>
                c.GetInvocationResult(It.IsAny<FunctionContext>()))
            .Returns(invocationResult.Object);
        httpRequestData
            .Setup(hrd => hrd.CreateResponse())
            .Returns(httpResponseData.Object);
        httpRequestData.Setup(hrd => hrd.Url).Returns(url);
        httpResponseData.Setup(hrd => hrd.Body).Returns(stream.Object);

        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        invocationResult.VerifySet(x => x.Value = httpResponseData.Object, Times.Once);
        httpResponseData.VerifySet(x => x.StatusCode = HttpStatusCode.Unauthorized, Times.Once);

        var bytes = "401 - Unauthorized!!!"u8.ToArray();
        stream.Verify(x => x.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task Invoke_WithBlobTrigger_ShouldContinue()
    {
        // Arrange
        const string trigger = "blobTrigger";
        
        var context = new Mock<FunctionContext>();
        var contextWrapper = new Mock<IFunctionContextWrapper>();
        var next = new Mock<FunctionExecutionDelegate>();
        
        context.Setup(c => c.FunctionDefinition.InputBindings.Values).Returns(GetMetaData(trigger));
        
        var middleware = new AuthorizeMiddleWare(contextWrapper.Object);
        
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