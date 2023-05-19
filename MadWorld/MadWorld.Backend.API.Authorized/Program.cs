using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.API.Shared.Response;
using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(configure =>
    {
        configure.UseMiddleware<AuthorizeMiddleWare>();
        configure.UseMiddleware<ResponseMiddleWare>();
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        services.AddApplication();
        services.AddInfrastructure();
        services.AddOpenApi(hostBuilder.HostingEnvironment.IsDevelopment());
        services.AddHealthChecks();
    })
    .Build();

await host.RunAsync();