using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Share.Helper.Cache;
using StructureMap;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static string GetMessage(MessageCase messageCase)
        {
            return GetMessage(messageCase, GetCurrentLanguageCode());
        }

        public static string GetMessage(MessageCase messageCase, string languageCode)
        {
            var cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
            var htmlContent = cacheHelper.GetHtmlContents().FirstOrDefault(i => i.Type == (short)messageCase
                                                                                 && i.LanguageCode == languageCode)
                            ?? cacheHelper.GetHtmlContents().FirstOrDefault(i => i.Type == (short)messageCase
                            && i.LanguageCode == GetDefaultLanguageIfNullOrEmpty());

            return htmlContent != null ? htmlContent.Content : string.Empty;
        }
    }
}
