using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.Dependencies;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.API.Shared.Response;
using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Infrastructure.Dependencies;
using MadWorld.Backend.Infrastructure.GraphExplorer;
using MadWorld.Shared.Contracts.Shared.Status;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.API.Authorized.Extensions;

public static class HostBuilderExtensions
{
    public static IHost BuildHost(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureFunctionsWorkerDefaults(configure =>
            {
                configure.UseMiddleware<AuthorizeMiddleWare>();
                configure.UseMiddleware<ResponseMiddleWare>();
            })
            .ConfigureServices((hostBuilderContext, services) =>
            {
                services.AddLogging();
                services.AddShared();
                services.AddApplication();
                services.AddInfrastructure();
                services.AddOpenApi(hostBuilderContext.HostingEnvironment.IsDevelopment());
                services.AddHealthChecks()
                    .AddCheck<GraphExplorerHealthCheck>(Services.GraphExplorer);
            })
            .Build();
    }
}