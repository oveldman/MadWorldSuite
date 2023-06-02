using MadWorld.Backend.API.Shared.Dependencies;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.API.Shared.Response;
using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.API.Anonymous.Extensions;

public static class HostBuilderExtensions
{
    public static IHost BuildHost(this IHostBuilder hostBuilder, ConfigurationOverrider? configurationOverrider = null)
    {
        return hostBuilder.ConfigureFunctionsWorkerDefaults(configure =>
            {
                configure.UseMiddleware<ResponseMiddleWare>();
            })
            .ConfigureServices((hostBuilder, services) =>
            {
                services.AddLogging();
                services.AddShared();
                services.AddApplication();
                services.AddInfrastructure();
                services.AddOpenApi(hostBuilder.HostingEnvironment.IsDevelopment());
                services.AddHealthChecks();
            })
            .Build();
    }
}