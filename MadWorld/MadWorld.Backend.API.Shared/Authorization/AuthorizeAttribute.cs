using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Azure.WebJobs.Host;

namespace MadWorld.Backend.API.Shared.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthorizeAttribute : Attribute
{
    public readonly RoleTypes Role;
    
    public AuthorizeAttribute(RoleTypes role)
    {
        Role = role;
    }
}