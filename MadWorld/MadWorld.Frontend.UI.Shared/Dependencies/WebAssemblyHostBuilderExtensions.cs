using MadWorld.Frontend.Application.Dependencies;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.Infrastructure.Dependencies;
using MadWorld.Frontend.UI.Shared.Security;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MadWorld.Frontend.UI.Shared.Dependencies;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddSuiteApp(this WebAssemblyHostBuilder builder)
    {
        builder.AddHttpClients();
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        builder.Services.AddMsalAuthentication(options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://nlMadWorld.onmicrosoft.com/4605ec9b-98b5-411b-b98b-d0a784221487/API.Access");
        }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, RemoteUserAccount, AccountExtraClaimsPrincipalFactory>();

        return builder;
    }
    
    private static void AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
        builder.AddConfigurationSettings();
        builder.AddAnonymousHttpClient();
        builder.AddAuthorizedHttpClient();
        builder.AddAuthorizedHttpClientWithoutToken();
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
    
    private static void AddAuthorizedHttpClientWithoutToken(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient(ApiTypes.MadWorldApiAuthorizedWithoutToken, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Authorized);
            }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddConfigurationSettings(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .AddOptions<ApiUrls>()
            .Configure(builder.Configuration.GetSection(ApiUrls.SectionName).Bind);
    }
}