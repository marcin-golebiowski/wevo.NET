using System;

namespace wevo.NET.Utils
{
    /**
    * System-based clock utility.
    */
    public class SystemClock : wevoClock
    {
        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public long GetCurrentTimeMillis()
        {
            return (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds);
        }
    }
}
