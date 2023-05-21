using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Account;

public class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IGraphExplorerClient _client;

    public GetAccountUseCase(IGraphExplorerClient client)
    {
        _client = client;
    }

    public Result<Option<GetAccountResponse>> GetAccount(GetAccountRequest request)
    {
        return new Result<Option<GetAccountResponse>>(new Exception("test"));
        
        return Option<GetAccountResponse>.Some(new GetAccountResponse());
    }
}