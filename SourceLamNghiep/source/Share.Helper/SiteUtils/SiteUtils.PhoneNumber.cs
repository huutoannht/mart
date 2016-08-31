using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.Helper.Models;
using ML.Common;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static string GetPhoneNumber(string phoneNumber)
        {
            if (!PhoneNumberIsValid(phoneNumber)) return phoneNumber;

            phoneNumber = phoneNumber.ToStr();

            if (phoneNumber.Length == 11) return phoneNumber;
            if (phoneNumber.Length == 10)
            {
                if (phoneNumber.IndexOf("0", StringComparison.Ordinal) > 0)
                {
                    return phoneNumber;
                }

                return "0" + phoneNumber;
            }

            return phoneNumber;
        }

        public static bool PhoneNumberIsValid(string phoneNumber)
        {
            phoneNumber = phoneNumber.ToStr();
            return 9 <= phoneNumber.Length && phoneNumber.Length <= 11;
        }

        public static List<TextValueModel> CountryCallingCodes()
        {
            return new List<TextValueModel>
            {
                new TextValueModel
                {
                    Text = "+1",
                    Value = "US"
                },
                new TextValueModel
                {
                    Text = "+84",
                    Value = "VN"
                },
                new TextValueModel
                {
                    Text = "+855",
                    Value = "KH"
                },
                new TextValueModel
                {
                    Text = "+856",
                    Value = "LA"
                },
                new TextValueModel
                {
                    Text = "+98",
                    Value = "IR"
                }
            };
        }

        public static string GetCountryCallingCode(string countryCode)
        {
            var item = CountryCallingCodes().FirstOrDefault(i => i.Value == countryCode);

            return item != null ? item.Text : string.Empty;
        }

        public static string GetFullPhoneNumber(string countryCode, string phoneNumber)
        {
            phoneNumber = GetPhoneNumber(phoneNumber);

            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length == 1) return string.Empty;

            if (phoneNumber[0] == '0') phoneNumber = phoneNumber.Substring(1, phoneNumber.Length - 1);

            var countryCallingCode = GetCountryCallingCode(countryCode);
            return countryCallingCode + phoneNumber;
        }
    }
}
