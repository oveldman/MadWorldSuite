using System.Runtime.CompilerServices;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.General;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Backend.Domain.Accounts;

public sealed class Account
{
    private const string RoleSplitValue = ";";
    
    public GuidId Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Roles { get; init; } = null!;
    public bool IsResourceOwner { get; init; }
    
    private Account() { }

    public static Result<Account> Parse(string id, string[] roles)
    {
        var rolesResult = ParseRoles(roles);
        if (rolesResult.IsFaulted) return new Result<Account>(rolesResult.GetException());
        
        return Parse(id, null!, rolesResult.GetValue(), false);
    }
    
    public static Result<Account> Parse(string id, string name, string roles, bool isResourceOwner)
    {
        var guidIdResult = GuidId.Parse(id);

        if (guidIdResult.IsFaulted) return new Result<Account>(guidIdResult.GetException());

        return new Account()
        {
            Id = guidIdResult.GetValue(),
            Name = name,
            Roles = roles,
            IsResourceOwner = isResourceOwner
        };
    }

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
            Roles = Roles.Split(RoleSplitValue).ToList(),
            IsResourceOwner = IsResourceOwner
        };
    }

    private static Result<string> ParseRoles(string[] roles)
    {
        roles = roles.Distinct().ToArray();
        var rolesValid = roles.All(r => Enum.TryParse(r, true, out RoleTypes _));
        if (!rolesValid) return new Result<string>(new ValidationException($"One of the {nameof(roles)} doesn't exist"));
        return string.Join(RoleSplitValue, roles);
    }
}