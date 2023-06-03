using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.General;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Frontend.Application.Accounts;

public sealed class PatchAccountUseCase : IPatchAccountUseCase
{
    private readonly IAccountService _service;
    
    public PatchAccountUseCase(IAccountService service)
    {
        _service = service;
    }

    public async Task<PatchResult> PatchAccount(Account account)
    {
        var request = CreateRequest(account);
        var result = await _service.PatchAccountAsync(request);

        return result.Match(
            _ => new PatchResult(),
            exception => new PatchResult(exception)
        );
    }
    
    private static PatchAccountRequest CreateRequest(Account account)
    {
        var roles = new List<string>()
        {
            RoleTypes.None.ToString()
        };
        
        if (account.HasUserRole) 
            roles.Add(RoleTypes.User.ToString());
        
        if (account is { HasAdminRole: true, HasUserRole: true }) 
            roles.Add(RoleTypes.Admin.ToString());
        
        return new PatchAccountRequest
        {
            Id = account.Id,
            Roles = roles.ToArray()
        };
    }
}