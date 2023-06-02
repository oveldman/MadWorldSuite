using System.Net;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Anonymous.Functions.CurriculumVitae;

public class GetCurriculumVitae
{
    private readonly IGetCurriculumVitaeUseCase _useCase;

    public GetCurriculumVitae(IGetCurriculumVitaeUseCase useCase)
    {
        _useCase = useCase;
    }

    [Function("GetCurriculumVitae")]
    [OpenApiOperation(operationId: "GetCurriculumVitae", tags: new[] { "CurriculumVitae" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetCurriculumVitaeResponse), Description = "The OK response")] 
    public GetCurriculumVitaeResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CurriculumVitae")] HttpRequestData request,
        FunctionContext executionContext)
    {
        return _useCase.GetCurriculumVitae();
    }
}