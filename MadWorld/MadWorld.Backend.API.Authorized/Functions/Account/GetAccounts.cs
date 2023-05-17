using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Authorized.Functions.Account;

public class GetAccounts
{
    private readonly IGetAccountsUseCase _useCase;

    public GetAccounts(IGetAccountsUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("GetAccounts")]
    [OpenApiOperation(operationId: "GetAccounts", tags: new[] { "Account" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountsResponse), Description = "The OK response")]
    public async Task<GetAccountsResponse> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Account/GetAll")] HttpRequestData req,
        FunctionContext executionContext)
    {
        return await _useCase.GetAccounts();
    }
}