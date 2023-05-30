using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Domain.Status;
using MadWorld.Shared.Contracts.Shared.Status;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MadWorld.Backend.Application.Status;

public class GetStatusUseCase : IGetStatusUseCase
{
    private readonly HealthCheckService _healthCheck;

    public GetStatusUseCase(HealthCheckService healthCheck)
    {
        _healthCheck = healthCheck;
    }
    
    public async Task<GetStatusResponse> GetStatus()
    {
        var healthReport = await _healthCheck.CheckHealthAsync();
        var unhealthyServices = healthReport
            .GetUnhealthyEntries()
            .Keys;
        
        return new GetStatusResponse(unhealthyServices);
    }
}