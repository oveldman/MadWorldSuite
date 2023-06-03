using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.CurriculumVitae;

public class PatchCurriculumVitae
{
    private readonly IPatchCurriculumVitaeUseCase _useCase;

    public PatchCurriculumVitae(IPatchCurriculumVitaeUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("PatchCurriculumVitae")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "PatchAccount", tags: new[] { "Account" })]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PatchCurriculumVitaeRequest), Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PatchCurriculumVitaeResponse), Description = "The OK response")]
    public async Task<Result<PatchCurriculumVitaeResponse>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData request,
        FunctionContext executionContext)
    {
        var patchAccountRequest = await request.ReadFromJsonAsync<PatchCurriculumVitaeRequest>();
        
        return _useCase.PatchCurriculumVitae(patchAccountRequest!);
    }
}