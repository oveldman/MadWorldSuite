using MadWorld.Frontend.Application.Accounts;
using MadWorld.Frontend.Domain.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Application.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetAccountUseCase, GetAccountUseCase>();
        services.AddScoped<IGetAccountsUseCase, GetAccountsUseCase>();
        services.AddScoped<IPatchAccountUseCase, PatchAccountUseCase>();
        
        return services;
    }
}