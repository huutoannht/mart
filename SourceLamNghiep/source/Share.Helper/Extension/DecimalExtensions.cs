using System;
using System.Globalization;

namespace Share.Helper.Extension
{
    public static class DecimalExtensions
    {
        public static string ToLocalMoneyString(this decimal val, bool removeDecimal = true, string cultureCode = null)
        {
            if (removeDecimal)
            {
                val = Math.Round(val, 0);
                return val.ToString(SiteSettings.CurrencyFormat).Replace(SiteSettings.NumberDecimalSeparator + "00", string.Empty);
            }

            return string.IsNullOrWhiteSpace(cultureCode) ? 
                val.ToString(SiteSettings.CurrencyFormat) :
                val.ToString(SiteSettings.CurrencyFormat, new CultureInfo(cultureCode));
        }

        public static string ToStringNonCulture(this double val)
        {
            return val.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
