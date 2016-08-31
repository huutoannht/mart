using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Backend.Library.Attribute
{
    public class DateRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime temp;
            return DateTime.TryParse(value.ToString(), out temp);
        }
    }
}