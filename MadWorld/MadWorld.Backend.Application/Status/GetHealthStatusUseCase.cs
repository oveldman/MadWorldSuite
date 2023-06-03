using MadWorld.Backend.Application.Extensions;
using MadWorld.Backend.Domain.Status;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Application.Status;

public sealed class GetHealthStatusUseCase : IGetHealthStatusUseCase
{
    private readonly ILogger<GetHealthStatusUseCase> _logger;
    private readonly HealthCheckService _healthCheck;

    public GetHealthStatusUseCase(ILogger<GetHealthStatusUseCase> logger, HealthCheckService healthCheck)
    {
        _logger = logger;
        _healthCheck = healthCheck;
    }
    
    public async Task<string> GetHealthStatus()
    {
        var healthReport = await _healthCheck.CheckHealthAsync();

        foreach (var (entryReportName, entryReportDetails) in healthReport.GetUnhealthyEntries())
        {
            _logger.LogWarning(entryReportDetails.Exception,
                "Health Report {EntryReportName} with status {Status}: {Description}. Duration: {Duration}",
                entryReportName, entryReportDetails.Status, entryReportDetails.Description, 
                entryReportDetails.Duration);
        }
        
        return healthReport.Status.ToString();
    }
}