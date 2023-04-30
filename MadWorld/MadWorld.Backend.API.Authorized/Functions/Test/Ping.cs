using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using MadWorld.Backend.API.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Test;

public static class Ping
{
    [Function("Ping")]
    [OpenApiOperation(operationId: "Ping", tags: new[] { "Test" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static HttpResponseData Run([Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var user = executionContext.GetUser();
        
        var logger = executionContext.GetLogger("Ping");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        
        response.WriteString($"Authorized: Welcome to Azure Functions, {user.Name}!");

        return response;
    }
}