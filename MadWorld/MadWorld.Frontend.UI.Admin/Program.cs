using MadWorld.Frontend.Application.Dependencies;
using MadWorld.Frontend.Infrastructure.Dependencies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MadWorld.Frontend.UI.Admin;
using MadWorld.Frontend.UI.Shared.Dependencies;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

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

await builder.Build().RunAsync();