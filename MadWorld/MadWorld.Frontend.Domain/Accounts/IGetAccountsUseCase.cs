using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IGetAccountsUseCase
{
    Task<IReadOnlyCollection<AccountContract>> GetAccountsAsync();
}