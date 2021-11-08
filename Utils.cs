using System;
namespace AvaloniaTest
{
    public class Utils
    {
        // https://stackoverflow.com/questions/2776673/how-do-i-truncate-a-net-string
        public static string? Truncate(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "..";
        }

        public static string FormatISO8602(string value)
        {
            DateTime dateISO8602 = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ",
                                System.Globalization.CultureInfo.InvariantCulture);
            return dateISO8602.ToString("dddd, dd MMMM yyyy");
        }
    }
}
