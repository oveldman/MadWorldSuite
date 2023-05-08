using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostBuilder, services) =>
    {
        services.AddApplication();
        services.AddOpenApi(hostBuilder.HostingEnvironment.IsDevelopment());
        services.AddHealthChecks();
    })
    .Build();

host.Run();