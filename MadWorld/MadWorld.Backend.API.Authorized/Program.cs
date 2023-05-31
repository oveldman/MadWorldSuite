using MadWorld.Backend.API.Authorized.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
                .BuildHost();

await host.RunAsync();