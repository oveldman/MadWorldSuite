using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IAccountManager
{
    Task<IReadOnlyCollection<AccountContract>> GetAccountsAsync();
}