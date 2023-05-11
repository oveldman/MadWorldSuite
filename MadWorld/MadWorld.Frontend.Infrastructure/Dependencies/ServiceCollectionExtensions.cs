using MadWorld.Frontend.Application.Status;
using MadWorld.Frontend.Application.Test;
using MadWorld.Frontend.Infrastructure.Status;
using MadWorld.Frontend.Infrastructure.Test;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Infrastructure.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPingService, PingService>();
        services.AddScoped<IStatusService, StatusService>();

        return services;
    }
}