using MadWorld.Backend.API.Shared.OpenAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((_, services) =>
    {
        const string openApiTitle = "Mad World API Anonymous";
        services.AddOpenApi(openApiTitle);
        services.AddHealthChecks();
    })
    .Build();

host.Run();