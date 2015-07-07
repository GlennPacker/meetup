using System;

namespace MeetUp.MeetUpApi.Helpers
{
    public static class ExtensionMethods
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt = epoch.AddMilliseconds(unixTime);
            if (TimeZoneInfo.Local.IsDaylightSavingTime(dt))
                dt = dt.AddHours(1);
            return dt;
        }

        public static double ToJavascriptDateTime(this DateTime date)
        {
            var result = (date - (new DateTime(1970, 1, 1))).TotalMilliseconds;
            return result;
        }
    }
}
