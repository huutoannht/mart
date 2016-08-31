using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Share.Messages;
using ML.Common;

namespace Share.Helper.Extension
{
    public static class StringExtensions
    {
        #region get resource

        private static CultureInfo GetCultureInfo(string languageCode)
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            if (!string.IsNullOrWhiteSpace(languageCode))
            {
                culture = new CultureInfo(languageCode);
            }
            return culture;
        }

        public static string GetBeRecoverPasswordRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BeScreens.RecoverPassword.RecoverPassword",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetBeRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BackendMessage",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetFeRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.FrontendMessage",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetDriverSignUpRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.DriverScreens.SignUp.SignUp",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetBeLoginRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BeScreens.Login.Login",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetBeSiteSettingRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BeScreens.SuperAdmin.SiteSetting.SiteSetting",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetBeRoleRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BeScreens.SuperAdmin.Role.Role",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetBeUserRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.BeScreens.Administrator.Administrator",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetEnumDesRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.Enums.Enums",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetServiceMessageRes(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.ServiceMessage.ServiceMessage",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetDeviceAppRiderUIText(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.DeviceApp.Rider.UIText",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetDeviceAppDriverUIText(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.DeviceApp.Driver.UIText",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        public static string GetDeviceAppCommonUIText(this string name, string languageCode = null)
        {
            var rm = new ResourceManager("Share.Messages.DeviceApp.Common.UIText",
                                       typeof(BackendMessage).Assembly);

            var value = rm.GetString(name, GetCultureInfo(languageCode));
            return value;
        }

        #endregion

        public static string Repeat(this string s, int times)
        {
            return string.Concat(Enumerable.Repeat(s, times));
        }

        public static string ToUrlFriendly(this string s)
        {
            s = s.ToStr();
            var sb = new StringBuilder();
            var wasHyphen = true;

            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLower(c));
                    wasHyphen = false;
                }
                else if (c != '\'' && !wasHyphen)
                {
                    sb.Append('-');
                    wasHyphen = true;
                }
            }

            // Avoid trailing hyphens
            if (wasHyphen && sb.Length > 0)
                sb.Length--;

            return sb.ToString().ToUnSign();
        }

        public static string GetValidDomain(this string domainName)
        {
            return SiteUtils.GetHttp() + domainName.ToStr()
                .Trim()
                .Replace("https://", string.Empty)
                .Replace("http://", string.Empty);
        }

        public static string BuildLink(this string domainName, string follow = null)
        {
            if (!string.IsNullOrWhiteSpace(follow))
            {
                return GetValidDomain(domainName) + "/" + follow;
            }

            return GetValidDomain(domainName);
        }

        public static string GetServerTimeZoneId(this string timezoneOffset)
        {
            if (timezoneOffset == "00:00" || string.IsNullOrWhiteSpace(timezoneOffset))
            {
                timezoneOffset = "(UTC)";
            }

            foreach (var info in TimeZoneInfo.GetSystemTimeZones())
            {
                if (info.DisplayName.Contains(timezoneOffset))
                {
                    return info.Id;
                }
            }

            return TimeZoneInfo.Local.Id;
        }

        public static string GetLast(this string source, int tailLength)
        {
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }

        public static bool IsBase64String(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        public static string ToUnSign(this string s)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
