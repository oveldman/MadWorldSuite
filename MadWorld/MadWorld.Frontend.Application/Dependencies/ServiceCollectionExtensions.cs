using MadWorld.Frontend.Application.Accounts;
using MadWorld.Frontend.Domain.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Application.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountManager, AccountManager>();
        
        return services;
    }
}