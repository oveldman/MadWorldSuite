using LanguageExt.Common;

namespace MadWorld.Backend.Application.LanguageExt;

public static class ResultExtensions
{
    public static Exception GetException<T>(this Result<T> result)
    {
        return result.Match(
            Succ: _ => null,
            Fail: exception => exception
        );
    }
}