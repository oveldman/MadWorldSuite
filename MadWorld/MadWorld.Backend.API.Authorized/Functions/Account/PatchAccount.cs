using System.Collections.Generic;
using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Functions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Account;

public sealed class PatchAccount
{
    private readonly IPatchAccountUseCase _useCase;
    
    public PatchAccount(IPatchAccountUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("PatchAccount")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "PatchAccount", tags: new[] { "Account" })]
    [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(PatchAccountRequest), Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OkResponse), Description = "The OK response")]
    public async Task<Result<OkResponse>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route="Account")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var request = await req.ReadFromJsonAsync<PatchAccountRequest>();
        return _useCase.PatchAccount(request);
    }
}