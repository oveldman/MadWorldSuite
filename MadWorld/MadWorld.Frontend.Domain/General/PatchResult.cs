namespace MadWorld.Frontend.Domain.General;

public sealed class PatchResult
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    public PatchResult()
    {
        IsSuccess = true;
    }

    public PatchResult(Exception exception)
    {
        IsSuccess = false;
        ErrorMessage = exception.Message;
    }
}