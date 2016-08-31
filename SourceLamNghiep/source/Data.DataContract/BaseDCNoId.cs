using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using ML.Common;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class BaseDCNoId
    {
        protected string GetEnumDescription(Enum obj)
        {
            try
            {
                var fieldInfo = obj.GetType().GetField(obj.ToString());

                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                var description = attr != null && !string.IsNullOrWhiteSpace(attr.Description)
                    ? attr.Description
                    : obj.ToString();

                return description;
            }
            catch (NullReferenceException ex)
            {
                return string.Empty;
            }
        }
    }
}
