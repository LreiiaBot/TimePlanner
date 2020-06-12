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

        public static string Format(this TimeSpan time)
        {
            if (time == null)
            {
                return "none";
            }
            var timeString = time.ToString(@"dd\.hh\:mm");
            return timeString;
        }

        public static Tuple<string, bool> ToSetLength(this string input, int maxSpace)
        {
            bool fitsIn = true;
            string output;
            if (input.Length > maxSpace)
            {
                output = input.Substring(0, maxSpace);
                fitsIn = false;
            }
            else
            {
                output = input.PadRight(maxSpace);
            }
            return new Tuple<string, bool>(output, fitsIn);
        }

        public static bool IsInThePast(this DateTimeOffset date)
        {
            bool past = false;
            if (date.Subtract(DateTimeOffset.Now) < TimeSpan.Zero)
            {
                past = true;
            }
            return past;
        }

        public static TimeSpan GetTimeLeft(this DateTimeOffset date)
        {
            return date.Subtract(DateTimeOffset.Now);
        }

        public static void Write(string text, ConsoleColor foreGround = ConsoleColor.White)
        {
            Console.ForegroundColor = foreGround;
            Console.Write(text);
        }
    }
}
