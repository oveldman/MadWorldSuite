using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Shared.Error;

public class ErrorResponse : IResponse
{
    public string Message { get; set; } = string.Empty;

    public static ErrorResponse CreateDefault()
    {
        return new()
        {
            Message = "There went something wrong. Please try again later."
        };
    }
}