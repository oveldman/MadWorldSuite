using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Authorized.Functions.Test;

public static class PingWithUsername
{
    [Authorize(RoleTypes.None)]
    [Function("PingWithUsername")]
    [OpenApiOperation(operationId: "PingWithUsername", tags: new[] { "Test" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static string Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var user = executionContext.GetUser();
        
        return $"Authorized: Welcome to Azure Functions, {user.Name}!!";
    }
}