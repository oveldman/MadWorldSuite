using LanguageExt;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Frontend.Application.Accounts;

public class GetAccountUseCase : IGetAccountUseCase
{
    private readonly IAccountService _service;

    public GetAccountUseCase(IAccountService service)
    {
        _service = service;
    }

    public async Task<Option<Account>> GetAccountAsync(string id)
    {
        var response = await _service.GetAccountAsync(id);

        return response.Match(
            r => ConvertToAccount(r.Account),
            _ = Option<Account>.None);
    }

    private static Account ConvertToAccount(AccountDetailContract contract)
    {
        var hasUserRole = contract.Roles.Contains(RoleTypes.User.ToString());
        var hasAdminRole = contract.Roles.Contains(RoleTypes.Admin.ToString());

        return new Account(
            contract.Id, 
            contract.Name, 
            hasAdminRole, 
            hasUserRole, 
            contract.IsResourceOwner);
    }
}