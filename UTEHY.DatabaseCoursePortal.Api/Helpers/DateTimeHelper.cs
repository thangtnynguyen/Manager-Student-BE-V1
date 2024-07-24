using System.Xml;

namespace UTEHY.DatabaseCoursePortal.Api.Helpers
{
    public static class DateTimeHelper
    {
        public static string ConvertIso8601ToHoursMinutesSeconds(string iso8601Duration)
        {
            TimeSpan duration = XmlConvert.ToTimeSpan(iso8601Duration);

            return $"{duration.Minutes:D2}:{duration.Seconds:D2}";
        }
    }
}
