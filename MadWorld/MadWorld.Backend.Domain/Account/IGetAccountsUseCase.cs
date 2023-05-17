using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Account;

public interface IGetAccountsUseCase
{
    GetAccountsResponse GetAccounts();
}