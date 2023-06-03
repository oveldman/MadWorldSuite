using System.Net;
using LanguageExt;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.CurriculumVitae;

public class GetCurriculumVitae
{
    private readonly IGetCurriculumVitaeUseCase _useCase;

    public GetCurriculumVitae(IGetCurriculumVitaeUseCase useCase)
    {
        _useCase = useCase;
    }

    [Authorize(RoleTypes.None)]
    [Function("GetCurriculumVitae")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetCurriculumVitae", tags: new[] { "CurriculumVitae" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetCurriculumVitaeResponse), Description = "The OK response")] 
    public Option<GetCurriculumVitaeResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CurriculumVitae")] HttpRequestData request,
        FunctionContext executionContext)
    {
        return _useCase.GetCurriculumVitae();
    }
}