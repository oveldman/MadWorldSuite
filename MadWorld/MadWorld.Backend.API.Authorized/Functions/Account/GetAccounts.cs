using System.Collections.Generic;
using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.Application.Account;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.API.Authorized.Functions.Account;

public class GetAccounts
{
    private readonly GetAccountsUseCase _useCase;

    public GetAccounts(GetAccountsUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("GetAccounts")]
    [OpenApiOperation(operationId: "GetAccounts", tags: new[] { "Account" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountsResponse), Description = "The OK response")]
    public GetAccountsResponse Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Account/GetAll")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return _useCase.GetAccounts();
    }
}