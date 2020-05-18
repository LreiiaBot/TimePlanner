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

        public static Tuple<string, bool> ToSetLength(this string input, int maxSpace)
        {
            bool fitsIn = true;
            string output;
            if (input.Length > maxSpace)
            {
                output = input.Substring(0, maxSpace);
                fitsIn = false;
            }
            else if (input.Length < maxSpace)
            {
                output = input.PadRight(maxSpace);
            }
            else
            {
                output = input;
            }
            return new Tuple<string, bool>(output, fitsIn);
        }
    }
}
