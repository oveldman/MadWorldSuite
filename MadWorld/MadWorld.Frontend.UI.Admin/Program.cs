using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MadWorld.Frontend.UI.Admin;
using MadWorld.Frontend.UI.Shared.Dependencies;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSuiteApp(builder.Configuration, builder.HostEnvironment);

await builder.Build().RunAsync();