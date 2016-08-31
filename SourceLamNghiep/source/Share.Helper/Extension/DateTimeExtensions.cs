using System;

namespace Share.Helper.Extension
{
    public static class DateTimeExtensions
    {
        public static string ToTimeString(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.ToString(SiteSettings.TimeFormat);
        }

        public static string ToTimeString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            if (dateTime.Value == DateTime.MinValue || dateTime.Value == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.Value.ToTimeString();
        }

        public static string ToDateTimeString(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.ToString(SiteSettings.DateTimeFormat);
        }

        public static string ToDateTimeString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            if (dateTime.Value == DateTime.MinValue || dateTime.Value == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.Value.ToDateTimeString();
        }

        public static string ToDateString(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.ToString(SiteSettings.DateFormat);
        }

        public static string ToDateString(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            if (dateTime.Value == DateTime.MinValue || dateTime.Value == DateTime.MaxValue)
            {
                return string.Empty;
            }

            return dateTime.Value.ToDateString();
        }
    }
}
