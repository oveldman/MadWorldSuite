using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using WireMock.Server;

namespace MadWorld.IntegrationTests.Startups;

public class UiStartupFactory : IAsyncDisposable
{
    public readonly WireMockServer MockServer;

    public UiStartupFactory()
    {
        MockServer = WireMockServer.Start();
    }
    
    public WebAssemblyHostConfiguration GetConfiguration()
    {
        var configuration = new WebAssemblyHostConfiguration();
        configuration.AddInMemoryCollection(new KeyValuePair<string, string?>[]
        {
            new("ApiUrls:Anonymous", $"{MockServer.Urls[0]}/anonymous/"),
            new("ApiUrls:Authorized", $"{MockServer.Urls[0]}/authorized/"),
            new("ApiUrls:BaseUrlAuthorized", "https://api.mad-world.nl")
        });
        
        return configuration;
    }
    
    public static IWebAssemblyHostEnvironment GetHostEnvironment()
    {
        return new FakeWebAssemblyHostEnvironment();
    }

    public static AccessTokenResult GetFakeAccessTokenResult()
    {
        return new AccessTokenResult(
            AccessTokenResultStatus.Success,
            new AccessToken(),
            "https://www.google.nl",
            new InteractiveRequestOptions()
            {
                Interaction = InteractionType.GetToken,
                ReturnUrl = "https://www.google.nl"
            }
        );
    }
    
    public virtual ValueTask DisposeAsync()
    {
        MockServer.Stop();
        MockServer.Dispose();
        
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}