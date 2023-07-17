using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Accounts;

public sealed class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IGraphExplorerClient _client;

    public GetAccountUseCase(IGraphExplorerClient client)
    {
        _client = client;
    }

    public Result<Option<GetAccountResponse>> GetAccount(GetAccountRequest request)
    {
        var userIdResult = GuidId.Parse(request.Id);

        return userIdResult.Match(
            GetAccount,
            _ => new Result<Option<GetAccountResponse>>(userIdResult.GetException())
        );
    }
    
    private Result<Option<GetAccountResponse>> GetAccount(GuidId userId)
    {
        var account = _client.GetUserAsync(userId).GetAwaiter().GetResult();
        return account.Select(a => new GetAccountResponse()
        {
            Account = a.ToDetailContract()
        });
    }
}