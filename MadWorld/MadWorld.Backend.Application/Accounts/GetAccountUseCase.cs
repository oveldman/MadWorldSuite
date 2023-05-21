using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.Application.LanguageExt;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.General;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Accounts;

public class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IGraphExplorerClient _client;

    public GetAccountUseCase(IGraphExplorerClient client)
    {
        _client = client;
    }

    public Result<Option<GetAccountResponse>> GetAccount(GetAccountRequest request)
    {
        var userIdResult = GuidId.Parse(request.Id);

        if (userIdResult.IsFaulted)
        {
            return new Result<Option<GetAccountResponse>>(userIdResult.GetException());
        }

        var account = Option<Account>.None;
        userIdResult.IfSucc(userId => account = _client.GetUserAsync(userId).GetAwaiter().GetResult());

        if (account.IsNone)
        {
            return Option<GetAccountResponse>.None;
        }

        return Option<GetAccountResponse>.Some(new GetAccountResponse()
        {
            Account = account.ValueUnsafe().ToDetailContract()
        });
    }
}