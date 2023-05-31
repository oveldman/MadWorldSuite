using MadWorld.Backend.API.Authorized.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests;

public class ApiStartupFactory
{
    public readonly IHost Host;

    public ApiStartupFactory()
    {
        var wireMockServer = WireMockServer.Start();

        Host = new HostBuilder()
            .ConfigureAppConfiguration(builder =>
            {
                Environment.SetEnvironmentVariable("AzureAD__ApplicationId", "ApplicationId");
                Environment.SetEnvironmentVariable("AzureAD__TenantId", "TenantId");
                Environment.SetEnvironmentVariable("AzureAD__ClientId", "ClientId");
                Environment.SetEnvironmentVariable("AzureAD__ClientSecret", "ClientSecret");
                Environment.SetEnvironmentVariable("GraphExplorer__BaseUrl", wireMockServer.Urls[0]);
                builder.AddEnvironmentVariables();
            })
            .ConfigureServices(collection =>
            {
                collection.AddSingleton(wireMockServer);
            })
            .BuildHost();
    }
}