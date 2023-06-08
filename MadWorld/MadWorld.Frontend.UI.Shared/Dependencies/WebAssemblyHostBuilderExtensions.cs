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
    public static IServiceCollection AddSuiteApp(
        this IServiceCollection services, 
        WebAssemblyHostConfiguration configuration, 
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddHttpClients(configuration, hostEnvironment);
        services.AddApplication();
        services.AddInfrastructure();

        services.AddMsalAuthentication(options =>
        {
            configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
            options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
            options.ProviderOptions.DefaultAccessTokenScopes.Add("https://nlMadWorld.onmicrosoft.com/4605ec9b-98b5-411b-b98b-d0a784221487/API.Access");
        }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, RemoteUserAccount, AccountExtraClaimsPrincipalFactory>();

        return services;
    }
    
    private static void AddHttpClients(
        this IServiceCollection services, 
        WebAssemblyHostConfiguration configuration, 
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(hostEnvironment.BaseAddress)});
        services.AddConfigurationSettings(configuration);
        services.AddAnonymousHttpClient();
        services.AddAuthorizedHttpClient();
        services.AddAuthorizedHttpClientWithoutToken();
    }

    private static void AddAnonymousHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient(ApiTypes.MadWorldApiAnonymous, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Anonymous);
            }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddAuthorizedHttpClient(this IServiceCollection services)
    {
        services.AddScoped<SuiteAuthorizedMessageHandler>();
        services.AddHttpClient(ApiTypes.MadWorldApiAuthorized, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Authorized);
            }).AddHttpMessageHandler<SuiteAuthorizedMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddAuthorizedHttpClientWithoutToken(this IServiceCollection services)
    {
        services.AddHttpClient(ApiTypes.MadWorldApiAuthorizedWithoutToken, (serviceProvider, client) =>
            {
                var apiUrlsOption = serviceProvider.GetService<IOptions<ApiUrls>>()!;
                client.BaseAddress = new Uri(apiUrlsOption.Value.Authorized);
            }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            .AddPolicyHandler(RetryPolicies.GetBadGateWayPolicy());
    }
    
    private static void AddConfigurationSettings(this IServiceCollection services, WebAssemblyHostConfiguration configuration)
    {
        services
            .AddOptions<ApiUrls>()
            .Configure(configuration.GetSection(ApiUrls.SectionName).Bind);
    }
}