using System.Linq;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Share.Helper.Cache;
using StructureMap;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static string GetDataTheme(SiteType siteType)
        {
            var cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
            var list = cacheHelper.GetAppSettings(AppSettingType.SiteStyleSettings);
            var dataThem = "blue";
            switch (siteType)
            {
                case SiteType.StoreSite:
                    dataThem = (list.FirstOrDefault(i => i.Name == ConstKeys.StoreSiteColor) ?? new AppSetting()).Value;
                    break;
                case SiteType.SuperSite:
                    dataThem = (list.FirstOrDefault(i => i.Name == ConstKeys.AdminSiteColor) ?? new AppSetting()).Value;
                    break;
            }

            return dataThem;
        }

        public static string GetFrontEndColorCode(string themeData)
        {
            switch (themeData)
            {
                case "green":
                    return "#5A9E25";
                case "blue":
                    return "#2582BD";
                case "red":
                    return "#C03425";
                case "dark-blue":
                    return "#2C3E50";
                case "orange":
                    return "#D86C0D";
            }
            return string.Empty;
        }
    }
}
