using System.Runtime.Serialization;

namespace MadWorld.Frontend.Domain.Api;

[Serializable]
public class ApiResponseException : Exception
{
    public ApiResponseException(string message) : base(message)
    {
    }
    
    protected ApiResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}