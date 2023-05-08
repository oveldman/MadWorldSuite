using MadWorld.Backend.Application.Status;
using MadWorld.Backend.Domain.Status;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetStatusUseCase, GetStatusUseCase>();
    }
}