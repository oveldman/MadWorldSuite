using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;

namespace MadWorld.Backend.Domain.Properties;

public sealed class GuidId : ValueObject
{
    private readonly Guid _id;

    private GuidId(Guid id)
    {
        _id = id;
    }

    public static Result<GuidId> Parse(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            return new GuidId(guid);
        }

        return new Result<GuidId>(new ValidationException($"{nameof(id)} is not valid"));
    }
    
    public static implicit operator string(GuidId id) => id._id.ToString();

    public static explicit operator GuidId(string id)
    {
        var guid = Guid.Parse(id);
        return new GuidId(guid);
    }
}