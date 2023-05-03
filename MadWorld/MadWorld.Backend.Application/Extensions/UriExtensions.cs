namespace MadWorld.Backend.Application.Extensions;

public static class UriExtensions
{
    public static bool IsLocalHost(this Uri? uri)
    {
        return uri?.AbsoluteUri.Contains("localhost") ?? false;
    }
}