using System.Runtime.Serialization;

namespace MadWorld.Backend.Domain.Exceptions;

[Serializable]
public sealed class TableStorageException : Exception
{
    public TableStorageException (string message) : base(message)
    {
    }
    
    private TableStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}