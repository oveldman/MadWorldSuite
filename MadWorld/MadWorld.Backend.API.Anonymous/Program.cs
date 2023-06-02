using MadWorld.Backend.API.Anonymous.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .BuildHost();

await host.RunAsync();