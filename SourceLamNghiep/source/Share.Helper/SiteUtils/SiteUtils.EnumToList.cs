using System.Collections.Generic;
using System.Linq;
using Data.DataContract;
using Share.Helper.Extension;
using Share.Helper.Models;
using ML.Common;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static List<EnumModel> GetStoreFrontPageSection()
        {
            return EnumHelper.ToValues<StoreFrontPageSection>().Select(s => new EnumModel
            {
                Name = ((StoreFrontPageSection)s).GetEnumDesRes(),
                Value = s
            }).ToList();
        }

        public static List<EnumModel> GetExternalType()
        {
            return EnumHelper.ToValues<ExternalType>().Select(s => new EnumModel
            {
                Name = ((ExternalType)s).GetEnumDesRes(),
                Value = s
            }).ToList();
        }

        public static List<EnumModel> GetPaymentMethod()
        {
            return EnumHelper.ToValues<PaymentMethod>().Select(s => new EnumModel
            {
                Name = ((PaymentMethod)s).GetEnumDesRes(),
                Value = s
            }).ToList();
        }

        public static List<EnumModel> GetWeekDay()
        {
            return EnumHelper.ToValues<WeekDay>().Select(s => new EnumModel
            {
                NameLocal = ((WeekDay)s).GetEnumDesRes(),
                Name = ((WeekDay)s).ToString(),
                Value = s
            }).ToList();
        }
    }
}
