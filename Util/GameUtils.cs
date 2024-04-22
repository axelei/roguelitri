using System;

namespace roguelitri.Util;

public static class GameUtils
{
    private static readonly DateTime Jan1St1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long EpochMillis()
    {
        return (long) (DateTime.UtcNow - Jan1St1970).TotalMilliseconds;
    }
    
}