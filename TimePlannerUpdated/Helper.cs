using System;

namespace TimePlannerUpdated
{
    static class Helper
    {
        public static string Format(this DateTimeOffset date)
        {
            var dateString = date.ToString("dd.MM.yyyy HH:mm");
            return dateString;
        }
    }
}
