using MadWorld.Frontend.Domain.Api;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Frontend.Infrastructure.Dependencies;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddHttpClients(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
        builder.AddAnonymousHttpClient();
        builder.AddAuthorizedHttpClient();
        
        return builder;
    }

    private static void AddAnonymousHttpClient(this WebAssemblyHostBuilder builder)
    {
        var apiUrlAnonymous = builder.Configuration["ApiUrls:Anonymous"];
        
        builder.Services.AddHttpClient(ApiTypes.MadWorldApiAnonymous, (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(apiUrlAnonymous!);
        }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
    
    private static void AddAuthorizedHttpClient(this WebAssemblyHostBuilder builder)
    {
        var apiUrlAuthorized = builder.Configuration["ApiUrls:Authorized"];
        
        builder.Services.AddHttpClient(ApiTypes.MadWorldApiAuthorized, (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(apiUrlAuthorized!);
        }).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
}