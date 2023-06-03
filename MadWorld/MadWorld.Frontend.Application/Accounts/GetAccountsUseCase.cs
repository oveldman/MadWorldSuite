using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Application.Accounts;

public sealed class GetAccountsUseCase : IGetAccountsUseCase
{
    private readonly IAccountService _service;

    public GetAccountsUseCase(IAccountService service)
    {
        _service = service;
    }

    public async Task<IReadOnlyCollection<AccountContract>> GetAccountsAsync()
    {
        var response = await _service.GetAccountsAsync();
        return response.Accounts;
    }
}