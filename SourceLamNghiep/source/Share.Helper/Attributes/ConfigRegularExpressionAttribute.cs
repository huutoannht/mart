using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Share.Helper.Attributes
{
    public class ConfigRegularExpressionAttribute : RegularExpressionAttribute
    {
        public ConfigRegularExpressionAttribute(string patternConfigKey)
            : base(ConfigurationManager.AppSettings[patternConfigKey])
        {
        }
    }
}