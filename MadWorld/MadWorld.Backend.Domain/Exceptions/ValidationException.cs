using System.Runtime.Serialization;

namespace MadWorld.Backend.Domain.Exceptions;

[Serializable]
public sealed class ValidationException : Exception
{
    public ValidationException (string message) : base(message)
    {
    }
    
    private ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}