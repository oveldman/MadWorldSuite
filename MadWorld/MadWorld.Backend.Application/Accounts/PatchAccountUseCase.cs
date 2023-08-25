using LanguageExt.Common;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Application.Accounts;

public sealed class PatchAccountUseCase : IPatchAccountUseCase
{
    private readonly IGraphExplorerClient _client;

    public PatchAccountUseCase(IGraphExplorerClient client)
    {
        _client = client;
    }
    
    public Result<OkResponse> PatchAccount(PatchAccountRequest? request)
    {
        if (request == null) return new Result<OkResponse>(new ValidationException("Request cannot be null"));
        var accountResult = Account.Parse(request.Id, request.Roles);

        return accountResult.Match(
            account => PatchAccount(account).GetAwaiter().GetResult(),
            _ => new Result<OkResponse>(accountResult.GetException())
        );
    }
    
    private async Task<Result<OkResponse>> PatchAccount(Account account)
    {
        var result = await _client.UpdateUser(account);
        return result.Match(
            _ => new OkResponse(),
            _ => new Result<OkResponse>(result.GetException())
        );
    }
}