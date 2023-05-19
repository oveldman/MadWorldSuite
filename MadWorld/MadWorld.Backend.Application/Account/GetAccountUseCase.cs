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

    public GetAccountResponse GetAccount(GetAccountRequest request)
    {
        return new GetAccountResponse();
    }
}