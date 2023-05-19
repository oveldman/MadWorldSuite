using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Account;

public class GetAccountsUseCase : IGetAccountsUseCase
{
    private readonly IGraphExplorerClient _graphExplorerClient;

    public GetAccountsUseCase(IGraphExplorerClient graphExplorerClient)
    {
        _graphExplorerClient = graphExplorerClient;
    }

    public async Task<GetAccountsResponse> GetAccounts()
    {
        var accounts = await _graphExplorerClient.GetUsersAsync();
        var accountContracts = accounts.Select(a => a.ToContract()).ToList();
        return new GetAccountsResponse(accountContracts);
    }
}