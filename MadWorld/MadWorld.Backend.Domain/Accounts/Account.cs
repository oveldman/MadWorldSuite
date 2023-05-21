using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Accounts;

public class Account
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Roles { get; init; } = null!;
    public bool IsResourceOwner { get; init; } = false;

    public AccountContract ToContract()
    {
        return new AccountContract()
        {
            Id = Id,
            Name = Name,
            IsResourceOwner = IsResourceOwner
        };
    }
    
    public AccountDetailContract ToDetailContract()
    {
        return new AccountDetailContract()
        {
            Id = Id,
            Name = Name,
            Roles = Roles.Split(';').ToList().AsReadOnly(),
            IsResourceOwner = IsResourceOwner
        };
    }
}