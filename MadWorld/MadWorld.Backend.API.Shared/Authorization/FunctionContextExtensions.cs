using System.Reflection;
using System.Security.Authentication;
using System.Security.Claims;
using MadWorld.Backend.API.Shared.Functions.Status;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.API.Shared.Authorization;

public static class FunctionContextExtensions
{
    private static readonly IReadOnlyCollection<string> AnonymousEndpoints = new List<string>()
    {
        nameof(HealthCheck),
        nameof(GetStatus),
        "Ping",
        "RenderOAuth2Redirect",
        "RenderOpenApiDocument",
        "RenderSwaggerDocument",
        "RenderSwaggerUI",
    };

    public static bool IsEndpointAnonymous(this FunctionContext context)
    {
        var azureFunctionName = context.FunctionDefinition.Name;
        return AnonymousEndpoints.Contains(azureFunctionName);
    }

    public static bool IsHttpTrigger(this FunctionContext context)
    {
        return context.FunctionDefinition.InputBindings.Values
            .First(a => a.Type.EndsWith("Trigger")).Type.Equals("httpTrigger", StringComparison.OrdinalIgnoreCase);
    }

    public static RoleTypes GetRequiredRole(this FunctionContext context)
    {
        var method = context.GetTargetFunctionMethod();
        var authorizeAttribute = method?.GetCustomAttributes()
            .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;

        return authorizeAttribute?.Role ?? RoleTypes.None;
    }

    public static User GetUser(this FunctionContext context)
    {
        var claimsPrincipal = context.Features.Get<ClaimsPrincipal>();
        return claimsPrincipal.GetUser();
    }
    
    private static MethodInfo? GetTargetFunctionMethod(this FunctionContext context)
    {
        // This contains the fully qualified name of the method
        // E.g. IsolatedFunctionAuth.TestFunctions.ScopesAndAppRoles
        var entryPoint = context.FunctionDefinition.EntryPoint;

        var assemblyPath = context.FunctionDefinition.PathToAssembly;
        var assembly = Assembly.LoadFrom(assemblyPath);
        var typeName = entryPoint[..entryPoint.LastIndexOf('.')];
        var type = assembly.GetType(typeName);
        var methodName = entryPoint[(entryPoint.LastIndexOf('.') + 1)..];
        return type?.GetMethod(methodName);
    }
}