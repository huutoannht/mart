using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Share.Helper.Models;
using Share.Messages.Enums;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static List<TextValueModel> CurrencyList()
        {
            var list = new List<TextValueModel>
            {
                new TextValueModel
                {
                    Value = "VND",
                    Text = Enums.Currency_VND
                },
                new TextValueModel
                {
                    Value = "USD",
                    Text = Enums.Currency_USD
                },
                new TextValueModel
                {
                    Value = "CNY",
                    Text = Enums.Currency_CNY
                }
            };

            return list.ToList();
        }

        public static string GetCurrencyName(string currencyCode)
        {
            var list = CurrencyList();
            return (list.FirstOrDefault(i => i.Value == currencyCode) ?? new TextValueModel()).Text;
        }

        public static bool CurrencyIsValid(string currencyCode)
        {
            return CurrencyList().Any(i => i.Value == currencyCode);
        }

        public static string GetCurrencySymbol(string currencyCode)
        {
            var symbolsByCode = new Dictionary<string, string>();

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                          .Select(x => new RegionInfo(x.LCID));

            foreach (var region in regions)
                if (!symbolsByCode.ContainsKey(region.ISOCurrencySymbol))
                    symbolsByCode.Add(region.ISOCurrencySymbol, region.CurrencySymbol);

            return symbolsByCode[currencyCode];
        }

        public static string GetCurrencyCodeByCountryCode(string countryCode)
        {
            var regionInfo = new RegionInfo(countryCode);
            return regionInfo.ISOCurrencySymbol;
        }

        public static string GetCurrencySymbolByCountryCode(string countryCode)
        {
            return GetCurrencySymbol(GetCurrencyCodeByCountryCode(countryCode));
        }

        public static string ConvertCurrencyStringToParseFormat(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            if (!value.Contains(",") && !value.Contains(".")) return value;

            value = value.Replace(" ", string.Empty);

            value = value.Replace(",", ".");

            var arr = value.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var last = arr.Last();

            arr.RemoveAt(arr.Count - 1);

            return string.Join(string.Empty, arr) + "." + last;
        }
    }
}
