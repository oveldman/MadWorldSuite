using LanguageExt.Common;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Accounts;

public sealed class PatchAccountUseCase : IPatchAccountUseCase
{
    private readonly IGraphExplorerClient _client;

    public PatchAccountUseCase(IGraphExplorerClient client)
    {
        _client = client;
    }
    
    public Result<PatchAccountResponse> PatchAccount(PatchAccountRequest? request)
    {
        if (request == null) return new Result<PatchAccountResponse>(new ValidationException("Request cannot be null"));
        var accountResult = Account.Parse(request.Id, request.Roles);

        return accountResult.Match(
            account => PatchAccount(account).GetAwaiter().GetResult(),
            _ => new Result<PatchAccountResponse>(accountResult.GetException())
        );
    }
    
    private async Task<Result<PatchAccountResponse>> PatchAccount(Account account)
    {
        var result = await _client.UpdateUser(account);
        return result.Match(
            _ => new PatchAccountResponse(),
            _ => new Result<PatchAccountResponse>(result.GetException())
        );
    }
}