using MadWorld.Frontend.Application.Status;
using MadWorld.Frontend.Application.Test;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Frontend.Infrastructure.Accounts;
using MadWorld.Frontend.Infrastructure.CurriculumVitae;
using MadWorld.Frontend.Infrastructure.Status;
using MadWorld.Frontend.Infrastructure.Test;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Infrastructure.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBlogService, BlogService.BlogService>();
        services.AddScoped<ICurriculumVitaeService, CurriculumVitaeService>();
        services.AddScoped<IPingService, PingService>();
        services.AddScoped<IStatusService, StatusService>();

        return services;
    }
}