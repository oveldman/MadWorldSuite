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
    
    public static Result<IValueObject> GetValueObjectResult<T>(this Result<T> result) 
        where T : IValueObject
    {
        return result.IsFaulted 
            ? new Result<IValueObject>(result.GetException()) 
            : result.GetValue();
    }
}