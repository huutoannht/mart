using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using Share.Helper.Models;
using Share.Messages;
using ML.Common;
using ML.Common.Extensions.Linq;

namespace Share.Helper.Extension
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum obj, bool friendlyName = true)
        {
            try
            {
                var fieldInfo = obj.GetType().GetField(obj.ToString());

                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                var description = attr != null && !string.IsNullOrWhiteSpace(attr.Description)
                    ? attr.Description
                    : obj.ToString();

                return friendlyName ? description.ToFriendlyCase() : description;
            }
            catch (NullReferenceException ex)
            {
                return string.Empty;
            }
        }

        public static string GetDescription(this object obj, bool friendlyName = true)
        {
            try
            {
                var fieldInfo = obj.GetType().GetField(obj.ToString());

                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                var description = attr != null && !string.IsNullOrWhiteSpace(attr.Description)
                    ? attr.Description
                    : obj.ToString();

                return friendlyName ? description.ToFriendlyCase() : description;
            }
            catch (NullReferenceException ex)
            {
                return string.Empty;
            }
        }

        public static string GetEnumDesRes(this Enum eEnum)
        {
            var rm = new ResourceManager("Share.Messages.Enums.Enums",
                                        typeof(BackendMessage).Assembly);

            var value = rm.GetString(eEnum.GetDescription(false), Thread.CurrentThread.CurrentCulture);
            return value;
        }

        public static List<EnumModel> EnumToList(this Type type, bool isLocalization = true)
        {
            var values = from Enum e in Enum.GetValues(type)
                         select e;

            var list = new List<EnumModel>();

            values.ForEach(e => list.Add(new EnumModel
            {
                Name = isLocalization ? e.GetEnumDesRes() : e.GetEnumDescription(false),
                Value = (short)(object)e
            }));

            return list.OrderBy(i => i.Name).ToList();
        }
    }
}