using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Application.Accounts;

public class AccountManager : IAccountManager
{
    private readonly IAccountService _service;

    public AccountManager(IAccountService service)
    {
        _service = service;
    }

    public async Task<IReadOnlyCollection<AccountContract>> GetAccountsAsync()
    {
        var response = await _service.GetAccountsAsync();
        return response.Accounts;
    }
}