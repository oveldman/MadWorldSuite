using MadWorld.Backend.Domain.Properties;

namespace MadWorld.Backend.Infrastructure.TableStorage.Extensions;

public static class DateTimeExtensions
{
    public static string ToRowKeyDesc(this DateTimeUtc utc)
    {
        var currentDateTime = (DateTime)utc;
        return $"{DateTime.MaxValue.Ticks - currentDateTime.Ticks:D19}";
    }
}