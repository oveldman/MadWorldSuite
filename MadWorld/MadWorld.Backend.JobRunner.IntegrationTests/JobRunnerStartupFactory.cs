using MadWorld.Backend.JobRunner.Extensions;
using MadWorld.IntegrationTests.Startups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MadWorld.Backend.JobRunner.IntegrationTests;

public class JobRunnerStartupFactory : ApiStartupFactory
{
    protected override IHost CreateHost()
    {
        PrepareHost();

        var host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                Environment.SetEnvironmentVariable("AzureWebJobsStorage", AzureConnectionString);
                Environment.SetEnvironmentVariable("AzureAD__ApplicationId", "ApplicationId");
                Environment.SetEnvironmentVariable("AzureAD__TenantId", "TenantId");
                Environment.SetEnvironmentVariable("AzureAD__ClientId", "ClientId");
                Environment.SetEnvironmentVariable("AzureAD__ClientSecret", "ClientSecret");
                builder.AddEnvironmentVariables();
            })
            .BuildHost();
        
        return host;
    }
}