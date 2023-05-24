using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IAccountService
{
    Task<GetAccountsResponse> GetAccountsAsync();
}