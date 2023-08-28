using MadWorld.Backend.JobRunner.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .BuildHost();

host.Run();