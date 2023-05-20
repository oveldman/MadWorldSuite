using MadWorld.Backend.API.Shared.Functions.Expansions;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.API.Shared.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<IFunctionContextWrapper, FunctionContextWrapper>();

        return services;
    }
}