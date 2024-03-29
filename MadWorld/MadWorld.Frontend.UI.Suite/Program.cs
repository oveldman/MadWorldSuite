using MadWorld.ExternPackages.Monaco.Dependencies;
using MadWorld.Frontend.UI.Shared.Dependencies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MadWorld.Frontend.UI.Suite;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMonaco();
builder.Services.AddSuiteApp(builder.Configuration, builder.HostEnvironment);
builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();