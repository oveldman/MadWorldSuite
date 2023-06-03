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
    
    public async Task<Result<PatchAccountResponse>> PatchAccount(PatchAccountRequest? request)
    {
        if (request == null) return new Result<PatchAccountResponse>(new ValidationException("Request cannot be null"));
        var accountResult = Account.Parse(request.Id, request.Roles);
        if (accountResult.IsFaulted) return new Result<PatchAccountResponse>(accountResult.GetException());

        var succeeded = await _client.UpdateUser(accountResult.GetValue());
        if (succeeded.IsFaulted) return new Result<PatchAccountResponse>(accountResult.GetException());
        
        return new PatchAccountResponse();
    }
}