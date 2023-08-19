namespace MadWorld.Backend.Infrastructure.TableStorage.Extensions;

public static class DateTimeExtensions
{
    public static string ToRowKeyDesc(this DateTime datetime)
    {
        return $"{DateTime.MaxValue.Ticks - datetime.Ticks:D19}";
    }
}