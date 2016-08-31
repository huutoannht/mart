using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Data.DataContract.BeUserDC;
using Share.Helper.Cache;
using Share.Helper.Extension;
using Share.Helper.Models;
using StructureMap;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static List<TextValueModel> LanguageListSupported(string languageCode = null)
        {
            var listSupported = new List<string>
            {
                "en-US","vi-VN"
            };

            return LanguageList(languageCode).Where(i => listSupported.Contains(i.Value)).ToList();
        }

        public static List<TextValueModel> LanguageList(string languageCode = null)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = GetCurrentLanguageCode();
            }

            var list = new List<TextValueModel>
            {
                new TextValueModel
                {
                    Text = "Vietnamese".GetBeRes(languageCode),
                    Value = "vi-VN"
                },
                new TextValueModel
                {
                    Text = "English".GetBeRes(languageCode),
                    Value = "en-US"
                }
            };

            var defaultLang = GetDefaultDisplayLanguage();

            var listResult = list.Where(i => i.Value == defaultLang).ToList();
            listResult.AddRange(list.Where(i => i.Value != defaultLang));

            return listResult;
        }

        public static string GetLanguageName(string code)
        {
            var langList = LanguageList();
            return (langList.FirstOrDefault(i => i.Value == code) ?? new TextValueModel()).Text;
        }

        private static string GetLanguageCodeFromCookie(HttpRequestBase request)
        {
            var code = GetDefaultLanguageIfNullOrEmpty();

            if (request.Cookies[ConstKeys.LanguageCode] != null &&
                !string.IsNullOrWhiteSpace(request.Cookies[ConstKeys.LanguageCode].Value)
                && LanguageCodeIsValid(request.Cookies[ConstKeys.LanguageCode].Value))
            {
                code = request.Cookies[ConstKeys.LanguageCode].Value;
            }

            return code;
        }

        private static string GetCurrentUserLanguageCodeWithRule(HttpRequestBase request, string preferLanguage)
        {
            var languageCodeFromCookie = GetLanguageCodeFromCookie(request);

            if (!request.IsAuthenticated) return languageCodeFromCookie;

            if (!string.IsNullOrWhiteSpace(languageCodeFromCookie)) return languageCodeFromCookie;
            if (!string.IsNullOrWhiteSpace(preferLanguage) && LanguageCodeIsValid(preferLanguage))
                return preferLanguage;
            return languageCodeFromCookie;
        }

        public static string GetCurrentBeUserLanguageCode(HttpRequestBase request, Guid currentUserId)
        {
            var cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
            var beUser = currentUserId != Guid.Empty ? cacheHelper.GetBeUser(currentUserId) : new BeUser();

            return GetCurrentUserLanguageCodeWithRule(request, beUser.PreferLanguage);
        }

        public static string GetCurrentStoreSiteLanguageCode(HttpRequestBase request)
        {
            return GetLanguageCodeFromCookie(request);
        }

        public static string GetCurrentLanguageCode()
        {
            var name = Thread.CurrentThread.CurrentUICulture.Name;
            return string.IsNullOrWhiteSpace(name) ? GetDefaultDisplayLanguage() : name;
        }

        public static bool IsViVn()
        {
            return GetCurrentLanguageCode() == "vi-VN";
        }

        public static bool IsCurrentLanguage(string langCode)
        {
            return GetCurrentLanguageCode() == langCode;
        }

        public static bool LanguageCodeIsValid(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;

            return LanguageListSupported().Any(i => i.Value == code);
        }

        public static string CurrentCountryCode()
        {
            var r = new RegionInfo(Thread.CurrentThread.CurrentCulture.LCID);
            return r.TwoLetterISORegionName;
        }

        public static List<TextValueModel> CountryList(string languageCode = null)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = GetCurrentLanguageCode();
            }

            var list = new List<TextValueModel>
            {
                new TextValueModel
                {
                    Text = "US".GetBeRes(languageCode),
                    Value = "US"
                },
                new TextValueModel
                {
                    Text = "VN".GetBeRes(languageCode),
                    Value = "VN"
                }
            };

            return list.OrderBy(i => i.Text).ToList();
        }

        public static string GetCountryName(string country, string languageCode = null)
        {
            var countryList = CountryList(languageCode);

            return (countryList.FirstOrDefault(i => i.Value == country) ?? new TextValueModel()).Text;
        }

        public static bool CountryCodeIsInvalid(string country)
        {
            return CountryList().Any(i => i.Value == country);
        }

        public static string GetDefaultLanguageIfNullOrEmpty(string language = null)
        {
            if (!string.IsNullOrWhiteSpace(language)) return language;
            return GetDefaultDisplayLanguage();
        }
    }
}
