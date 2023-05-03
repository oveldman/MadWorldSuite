using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.API.Shared.Functions;

public class HealthCheck
{
    private readonly HealthCheckService _healthCheck;
    public HealthCheck(HealthCheckService healthCheck)
    {
        _healthCheck = healthCheck;
    }
    
    [Function(nameof(HealthCheck))]
    [OpenApiOperation(operationId: nameof(HealthCheck), tags: new[] { "Status" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")] 
    public async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(HealthCheck));
        logger.LogInformation("Received heartbeat request");

        var status = await _healthCheck.CheckHealthAsync();
        return status.Status.ToString();
    }
}