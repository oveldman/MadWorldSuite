using System.Net;
using MadWorld.Backend.Domain.Status;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.API.Shared.Functions.Status;

public class HealthCheck
{
    private readonly IGetHealthStatusUseCase _useCase;
    public HealthCheck(IGetHealthStatusUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Function(nameof(HealthCheck))]
    [OpenApiOperation(operationId: nameof(HealthCheck), tags: new[] { "Status" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")] 
    public async Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(HealthCheck));
        logger.LogInformation("Received heartbeat request");

        return await _useCase.GetHealthStatus();
    }
}