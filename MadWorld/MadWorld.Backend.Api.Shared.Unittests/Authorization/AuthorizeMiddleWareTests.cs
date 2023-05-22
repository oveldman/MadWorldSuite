using System.Security.Claims;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.Functions.Status;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.Api.Shared.Unittests.Authorization;

public class AuthorizeMiddleWareTests
{
    [Fact]
    public async Task Invoke_GivenBearerRequest_BearerParsedSuccessfully()
    {
        // Arrange
        const string headers = @"{
                            'Authorization':'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHRlbnNpb25fcm9sZXMiOiJub25lO3VzZXI7YWRtaW4ifQ.3bXSGSI2JxWEj_eCstdpWh-0my1jhCJSl_hf61oAZSI'
                        }";

        const string functionTypeName = $"MadWorld.Backend.API.Shared.Functions.Status{nameof(GetStatus)}.Run";
        
        var assemblyLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MadWorld.Backend.API.Shared.dll");

        
        var context = new Mock<FunctionContext>();
        var next = new Mock<FunctionExecutionDelegate>();
        var features = new Mock<IInvocationFeatures>();
        
        var middleware = new AuthorizeMiddleWare();
        context.Setup(c => c.BindingContext.BindingData["Headers"]).Returns(headers);
        context.Setup(c => c.Features).Returns(features.Object);
        context.Setup(c => c.FunctionDefinition.EntryPoint).Returns(functionTypeName);
        context.Setup(c => c.FunctionDefinition.PathToAssembly).Returns(assemblyLocation);

        // Act
        await middleware.Invoke(context.Object, next.Object);
        
        // Assert
        next.Verify(n => n.Invoke(It.IsAny<FunctionContext>()), Times.Once());
        context.Verify(c => c.Features.Set(It.IsAny<ClaimsPrincipal>()), Times.Once());
    }
}