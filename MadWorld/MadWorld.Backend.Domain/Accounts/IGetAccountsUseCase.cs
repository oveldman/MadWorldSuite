using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Accounts;

public interface IGetAccountsUseCase
{
    Task<GetAccountsResponse> GetAccounts();
}