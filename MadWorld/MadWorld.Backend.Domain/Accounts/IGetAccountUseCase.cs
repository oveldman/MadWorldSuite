using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Accounts;

public interface IGetAccountUseCase
{
    GetAccountResponse GetAccount(GetAccountRequest request);
}