using System;
using System.Globalization;
using System.Web.Mvc;
using ML.Common;

namespace Share.Helper.ModelBinder
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            var modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            try
            {
                var val = valueResult.AttemptedValue;
                if (string.IsNullOrWhiteSpace(val))
                {
                    val = "0";
                }

                actualValue = Convert.ToDecimal(val.Replace(" ", string.Empty), CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
