using MadWorld.Backend.Application.Accounts;
using MadWorld.Backend.Application.CurriculumVitae;
using MadWorld.Backend.Application.Status;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.Status;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetAccountsUseCase, GetAccountsUseCase>();
        services.AddScoped<IGetAccountUseCase, GetAccountUseCase>();
        services.AddScoped<IPatchAccountUseCase, PatchAccountUseCase>();
        services.AddScoped<IGetCurriculumVitaeUseCase, GetCurriculumVitaeUseCase>();
        services.AddScoped<IGetHealthStatusUseCase, GetHealthStatusUseCase>();
        services.AddScoped<IGetStatusUseCase, GetStatusUseCase>();
    }
}