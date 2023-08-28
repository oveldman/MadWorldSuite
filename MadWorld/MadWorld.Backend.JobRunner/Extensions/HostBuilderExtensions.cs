using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.JobRunner.Extensions;

public static class HostBuilderExtensions
{
    public static IHost BuildHost(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((hostBuilderContext, services) =>
            {
                services.AddLogging();
                services.AddApplication();
                services.AddInfrastructure();
            })
            .Build();
    }
}