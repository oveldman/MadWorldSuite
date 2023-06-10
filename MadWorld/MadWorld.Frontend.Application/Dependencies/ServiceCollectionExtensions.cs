using MadWorld.Frontend.Application.Accounts;
using MadWorld.Frontend.Application.CurriculaVitae;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.CurriculaVitae;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Application.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetAccountUseCase, GetAccountUseCase>();
        services.AddScoped<IGetAccountsUseCase, GetAccountsUseCase>();
        services.AddScoped<IGetCurriculumVitaeUseCase, GetCurriculumVitaeUseCase>();
        services.AddScoped<IPatchAccountUseCase, PatchAccountUseCase>();
        services.AddScoped<IPatchCurriculumVitaeUseCase, PatchCurriculumVitaeUseCase>();
        
        return services;
    }
}