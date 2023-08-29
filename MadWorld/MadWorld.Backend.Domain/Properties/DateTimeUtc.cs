using MadWorld.Backend.Domain.System;

namespace MadWorld.Backend.Domain.Properties;

public class DateTimeUtc
{
    private readonly DateTime _dateTime;

    private DateTimeUtc(DateTime dateTime)
    {
        var utc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        _dateTime = utc;
    }

    public static DateTimeUtc Parse(DateTime dateTime)
    {
        return new DateTimeUtc(dateTime);
    }

    public static DateTimeUtc Now()
    {
        var now = SystemTime.Now();
        return new DateTimeUtc(now);
    }
    
    public static implicit operator DateTime(DateTimeUtc utc) => utc._dateTime;

    public static explicit operator DateTimeUtc(DateTime dateTime)
    {
        return new DateTimeUtc(dateTime);
    }
}