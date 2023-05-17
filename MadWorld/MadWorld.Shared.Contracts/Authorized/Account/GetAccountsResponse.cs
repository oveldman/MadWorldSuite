namespace MadWorld.Shared.Contracts.Authorized.Account;

public class GetAccountsResponse
{
    public IReadOnlyCollection<AccountContract> Accounts { get; private set; }
    
    public GetAccountsResponse(IReadOnlyCollection<AccountContract> accounts)
    {
        Accounts = accounts;
    }
}