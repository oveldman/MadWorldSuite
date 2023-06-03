using System.Collections.ObjectModel;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public sealed class AccountContract
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsResourceOwner { get; set; } = false;
}