using System;
using System.Globalization;
using System.Web.Mvc;

namespace Share.Helper.ModelBinder
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
           ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            var modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (!string.IsNullOrWhiteSpace(valueResult.AttemptedValue))
            {
                try
                {
                    actualValue = DateTime.ParseExact(valueResult.AttemptedValue,
                            SiteSettings.DateFormatJS.Replace("D", "d").Replace("Y", "y"), CultureInfo.InvariantCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
