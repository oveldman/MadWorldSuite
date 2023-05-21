using System.Collections.ObjectModel;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public class AccountDetailContract
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ReadOnlyCollection<string> Roles { get; set; } = null!;
    public bool IsResourceOwner { get; set; } = false;
}