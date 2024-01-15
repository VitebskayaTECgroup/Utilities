using System;

namespace Checkpoint.Models
{
    public static class TimespanExtentions
    {
        public static string ToTime(this TimeSpan ts, bool detailed = false)
        {
            if (ts == TimeSpan.Zero) return "";

            if (!detailed) return ts.ToString();

            if (ts.TotalMinutes >= 1)
            {
                string text = "";
                if (ts.Days > 0) text += ts.Days.ToString() + " дн. ";
                if (ts.Hours > 0) text += ts.Hours.ToString() + " ч. ";
                if (ts.Minutes > 0) text += ts.Minutes.ToString() + " мин. ";
                return text;
            }

            return "меньше 1 мин.";
        }

        public static bool Empty(this TimeSpan ts) => ts == TimeSpan.Zero;
    }
}