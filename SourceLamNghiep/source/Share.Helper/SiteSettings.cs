using System.Globalization;

namespace Share.Helper
{
    public static class SiteSettings
    {
        public const string HashAlgorithm = "SHA256";

        public const int DefaultMaxDisplayPages = 6;

        public const string DefaultPageQueryParameter = "PageIndex";

        public const int DefaultPageCount = 5;

        public const int DefaultPageSize = 20;

        public static string DateFormat
        {
            get
            {
                return "MM/dd/yyyy";
            }
        }

        public static string TimeFormat
        {
            get
            {
                return "hh:mm tt";
            }
        }

        public static string MonthFormat
        {
            get
            {
                return "MM/yyyy";
            }
        }

        public static string DateTimeFormat
        {
            get
            {
                return "MM/dd/yyyy hh:mm tt";
            }
        }

        public static string DateFormatJS
        {
            get
            {
                return "DD/MM/YYYY";
            }
        }

        public static string CSToJSDateFormat
        {
            get { return DateFormatJS.Replace("D", "d").Replace("Y", "y"); }
        }

        public const int DefaultDecimalPlaces = 2;

        public static string NumberDecimalSeparator
        {
            get
            {
                return CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator;
            }
        }

        public static string NumberGroupSeparator
        {
            get
            {
                return CultureInfo.CurrentUICulture.NumberFormat.CurrencyGroupSeparator;
            }
        }

        public static string CurrencyFormat
        {
            get { return "n"; }
        }

        public static string CurrencyForPayment(string countryCode)
        {
            return SiteUtils.GetCurrencyCodeByCountryCode(countryCode);
        }

        public static int ProductRatingMax = 5;
    }
}
