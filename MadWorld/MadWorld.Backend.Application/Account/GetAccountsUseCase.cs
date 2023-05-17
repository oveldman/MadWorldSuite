using MadWorld.Backend.Domain.Account;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Application.Account;

public class GetAccountsUseCase : IGetAccountsUseCase
{
    public GetAccountsResponse GetAccounts()
    {
        return new GetAccountsResponse(new List<AccountContract>());
    }
}