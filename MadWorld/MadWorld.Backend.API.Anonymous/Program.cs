using MadWorld.Backend.API.Shared.OpenAPI;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        const string openApiTitle = "Mad World API Anonymous";
        services.AddOpenApi(openApiTitle);
    })
    .Build();

host.Run();