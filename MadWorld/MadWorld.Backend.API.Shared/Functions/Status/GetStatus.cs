using System.Net;
using MadWorld.Backend.Domain.Status;
using MadWorld.Shared.Contracts.Shared.Status;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Shared.Functions.Status;

public sealed class GetStatus
{
    private readonly IGetStatusUseCase _useCase;
    
    public GetStatus(IGetStatusUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Function(nameof(GetStatus))]
    [OpenApiOperation(operationId: nameof(GetStatus), tags: new[] { "Status" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetStatusResponse), Description = "Returned all statuses from services")] 
    public async Task<GetStatusResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return await _useCase.GetStatus();
    }
}