using System.Collections.Generic;
using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Account;

public static class PatchAccount
{
    [Authorize(RoleTypes.Admin)]
    [Function("PatchAccount")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.ApiKey, Name = "authorization", In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "PatchAccount", tags: new[] { "Account" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "patch", Route="Account/Patch/{id}")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("PatchAccount");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
        
    }
}