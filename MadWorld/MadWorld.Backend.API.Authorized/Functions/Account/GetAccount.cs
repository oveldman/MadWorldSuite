using System.Net;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Account;

public sealed class GetAccount
{
    private readonly IGetAccountUseCase _useCase;

    public GetAccount(IGetAccountUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("GetAccount")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetAccount", tags: new[] { "Account" })]
    [OpenApiParameter("id", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountResponse), Description = "The OK response")]
    public Result<Option<GetAccountResponse>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Account/{id}")] HttpRequestData req,
        FunctionContext executionContext, 
        string id)
    {
        var request = new GetAccountRequest()
        {
            Id = id
        };

        return _useCase.GetAccount(request);
    }
}