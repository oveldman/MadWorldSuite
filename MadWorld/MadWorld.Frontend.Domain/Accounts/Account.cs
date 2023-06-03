namespace MadWorld.Frontend.Domain.Accounts;

public sealed class Account
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool HasAdminRole { get; set; }
    public bool HasUserRole { get; set; }
    public bool IsResourceOwner { get; set; }

    public Account(string id, string name, bool hasAdminRole, bool hasUserRole, bool isResourceOwner)
    {
        Id = id;
        Name = name;
        HasAdminRole = hasAdminRole;
        HasUserRole = hasUserRole;
        IsResourceOwner = isResourceOwner;
    }
}