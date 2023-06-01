using System.Text;
using System.Text.Json;

namespace MadWorld.IntegrationTests.Extensions;

public static class RequestExtensions
{
    public static MemoryStream ToMemoryStream<T>(this T request)
    {
        var byteArray = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(request));
        return new MemoryStream(byteArray);
    }
}