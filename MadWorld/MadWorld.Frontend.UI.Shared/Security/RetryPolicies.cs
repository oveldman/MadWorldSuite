using Polly;
using Polly.Extensions.Http;

namespace MadWorld.Frontend.UI.Shared.Security;

public static class RetryPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetBadGateWayPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.IsGetRequest() && msg.IsBadGateway())
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));
    }
    
    private static bool IsGetRequest(this HttpResponseMessage msg)
    {
        return msg.RequestMessage?.Method == HttpMethod.Get;
    }
    
    private static bool IsBadGateway(this HttpResponseMessage msg)
    {
        return msg.StatusCode == System.Net.HttpStatusCode.BadGateway;
    }
}