using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MadWorld.Backend.Application.Extensions;

public static class HealthReportExtensions
{
    public static IReadOnlyDictionary<string, HealthReportEntry> GetUnhealthyEntries(this HealthReport healthReport)
    {
        return healthReport.Entries
            .Where(e => e.Value.Status != HealthStatus.Healthy)
            .ToDictionary(s => s.Key, s => s.Value);
    }
}