using MadWorld.Frontend.UI.Shared.Dependencies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MadWorld.Frontend.UI.Suite;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddSuiteApp();

await builder.Build().RunAsync();