namespace MadWorld.Backend.Domain.Status;

public interface IGetHealthStatusUseCase
{
    Task<string> GetHealthStatus();
}