using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Backend.API.Shared.Authorization;

public class User
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Roles { get; init; } = string.Empty;
    
    public bool IsInRole(RoleTypes role)
    {
        return role == RoleTypes.None || Roles.Contains(role.ToString());
    }
}