using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.UI.Shared.Security;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadWorld.Frontend.UI.Shared.Dependencies;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
        builder.AddConfigurationSettings();
        builder.AddAnonymousHttpClient();
        builder.AddAuthorizedHttpClient();
        
        return builder;
    }

    private static void AddAnonymousHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient(ApiTypes.MadWorldApiAnonymous, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Anonymous);
            }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddAuthorizedHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<SuiteAuthorizedMessageHandler>();
        builder.Services.AddHttpClient(ApiTypes.MadWorldApiAuthorized, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Authorized);
            }).AddHttpMessageHandler<SuiteAuthorizedMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddConfigurationSettings(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .AddOptions<ApiUrls>()
            .Configure(builder.Configuration.GetSection(ApiUrls.SectionName).Bind);
    }
}