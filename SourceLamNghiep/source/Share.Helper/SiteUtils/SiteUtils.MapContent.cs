using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ML.Common;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static string MapContent(string content, Dictionary<string, string> mapKeys)
        {
            foreach (var mapKey in mapKeys)
            {
                content = Regex.Replace(content, EscapeSpecialCharacters(mapKey.Key), mapKey.Value ?? "", RegexOptions.IgnoreCase);
            }

            return content;
        }

        public static string EscapeSpecialCharacters(string key)
        {
            return Regex.Replace(key, @"[+\-&|!(){}[\]^""~*?:\\]", "\\$0");
        }

        public static string GetShopId(Guid id)
        {
            var str = id.ToStr().Replace("-", string.Empty);
            return str.Substring(str.Length - 13);
        }
    }
}
