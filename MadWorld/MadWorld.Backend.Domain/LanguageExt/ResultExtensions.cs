using LanguageExt.Common;

namespace MadWorld.Backend.Domain.LanguageExt;

public static class ResultExtensions
{
    public static Exception GetException<T>(this Result<T> result)
    {
        return result.Match(
            Succ: _ => null!,
            Fail: exception => exception
        );
    }
    
    public static T GetValue<T>(this Result<T> result)
    {
        return result.Match(
            Succ: t => t,
            Fail: _ => default!
        );
    }
    
    public static Result<ValueObject> GetValueObjectResult<T>(this Result<T> result) 
        where T : ValueObject
    {
        return result.IsFaulted 
            ? new Result<ValueObject>(result.GetException()) 
            : result.GetValue();
    }
}