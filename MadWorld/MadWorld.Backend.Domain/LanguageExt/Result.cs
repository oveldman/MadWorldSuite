using LanguageExt.Common;

namespace MadWorld.Backend.Domain.LanguageExt;

public static class Result
{
    public static bool HasFaultyState(out Exception exception, params Result<ValueObject>[] results)
    {
        exception = null!;

        if (!results.Any(r => r.IsFaulted)) return false;
        
        exception = results
            .First(r => r.IsFaulted)
            .GetException();
        return true;
    }
}