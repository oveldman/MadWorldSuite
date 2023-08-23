namespace MadWorld.Shared.Contracts.Shared.Functions;

public class OkResponse : IResponse
{
    public bool IsSuccess { get; } = true;
    public string Message { get; set; } = string.Empty;
}