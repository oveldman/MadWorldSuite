using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(configure =>
    {
        configure.UseMiddleware<AuthorizeMiddleWare>();
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        const string openApiTitle = "Mad World API Authorized";
        services.AddOpenApi(openApiTitle);
    })
    .Build();

host.Run();