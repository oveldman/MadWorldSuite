using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public class GetAccountsResponse : IResponse
{
    public IReadOnlyCollection<AccountContract> Accounts { get; private set; }
    
    public GetAccountsResponse(IReadOnlyCollection<AccountContract> accounts)
    {
        Accounts = accounts;
    }
}