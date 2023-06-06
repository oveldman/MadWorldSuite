using MadWorld.Backend.API.Authorized.Extensions;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.IntegrationTests.Startups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests;

public class AuthorizedApiStartupFactory : ApiStartupFactory
{
    protected override IHost CreateHost()
    {
        PrepareHost();

        var wireMockServer = WireMockServer.Start();

        var configurationOverrider = new ConfigurationOverrider()
        {
            GraphExplorerBaseUrl = wireMockServer.Urls[0]
        };

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
            .ConfigureServices(collection => { collection.AddSingleton(wireMockServer); })
            .BuildHost(configurationOverrider);
        
        return host;
    }
}