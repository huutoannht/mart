using System.Globalization;
using Share.Helper;
using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Share.Helper.Extension
{
    public static class HtmlHelperInputFunctions
    {
        private const string PhoneCssClass = "fa fa-phone";

        #region ZipCode
        public static MvcHtmlString TextBoxZipCodeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToZipCodeNumber();

            return TextBoxBuilder("mlInputZipCode", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxZipCode(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToZipCodeNumber();

            return TextBoxBuilder("mlInputZipCode", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }
        #endregion

        #region Credit Card
        public static MvcHtmlString TextBoxCreditCardNumberFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToZipCodeNumber();

            return TextBoxBuilder("mlInputCreditCardNumber", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxCreditCardNumber(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToZipCodeNumber();

            return TextBoxBuilder("mlInputCreditCardNumber", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }
        public static MvcHtmlString TextBoxCVSFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToZipCodeNumber();

            return TextBoxBuilder("mlInputCVS", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxCVS(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToZipCodeNumber();

            return TextBoxBuilder("mlInputCVS", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }
        #endregion

        #region Phone && SSN

        public static MvcHtmlString TextBoxPhoneFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToPhoneNumber();

            return TextBoxBuilder("mlInputPhone", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes), null, false, false, PhoneCssClass);
        }

        public static MvcHtmlString TextBoxPhone(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToPhoneNumber();

            return TextBoxBuilder("mlInputPhone", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes), null, false, false, PhoneCssClass);
        }

        public static MvcHtmlString TextBoxSsnFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToSsnNumber();

            return TextBoxBuilder("mlInputSsn", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxSsn(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToSsnNumber();

            return TextBoxBuilder("mlInputSsn", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        #endregion

        #region Tag

        public static MvcHtmlString TextBoxTagFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return TextBoxBuilder("ecTagsInput", htmlHelper, metadata, fullHtmlFieldName, metadata.Model, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxTag(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            return TextBoxBuilder("ecTagsInput", htmlHelper, null, name, value, GetHtmlAttributesSafely(htmlAttributes));
        }

        #endregion

        #region Date & Time

        public static MvcHtmlString TextBoxDateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            var model = metadata.Model.ToDateTimeNull();
            if (model != null && model != DateTime.MinValue) tmpValue = model.Value.ToString(SiteSettings.CSToJSDateFormat);

            return TextBoxBuilder("date-picker", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxFromDateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            var model = metadata.Model.ToDateTimeNull();
            if (model != null && model != DateTime.MinValue) tmpValue = model.Value.ToString(SiteSettings.CSToJSDateFormat);

            return TextBoxBuilder("from-date", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxToDateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            var model = metadata.Model.ToDateTimeNull();
            if (model != null && model != DateTime.MinValue) tmpValue = model.Value.ToString(SiteSettings.CSToJSDateFormat);

            return TextBoxBuilder("to-date", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxDate(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            var dateValue = value.ToDateTimeNull();
            if (dateValue != null) value = dateValue.Value.ToString(SiteSettings.CSToJSDateFormat);

            return TextBoxBuilder("date-picker", htmlHelper, null, name, value, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            var model = metadata.Model.ToDateTimeNull();
            if (model != null) tmpValue = model.Value.ToString(SiteSettings.TimeFormat);

            return TextBoxBuilder("timepicker", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxTime(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            var dateValue = value.ToDateTimeNull();
            if (dateValue != null) value = dateValue.Value.ToString(SiteSettings.TimeFormat);

            return TextBoxBuilder("timepicker", htmlHelper, null, name, value, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString DateTimeHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            var model = metadata.Model.ToDateTimeNull();
            if (SiteUtils.GetCurrentLanguageCode() == "en-US")
            {
                if (model != null && model != DateTime.MinValue) tmpValue = model.Value.ToDateString();
            }
            else
            {
                if (model != null && model != DateTime.MinValue) tmpValue = model.Value.ToString(SiteSettings.CSToJSDateFormat);
            }

            return HiddenBuilder(htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        #endregion

        #region Numeric

        public static MvcHtmlString TextBoxNumericFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToStringNumber();

            return TextBoxBuilder("mlInputNumeric", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxNumeric(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null)
        {
            return TextBoxBuilder("mlInputNumeric", htmlHelper, null, name, value.ToStringNumber(), GetHtmlAttributesSafely(htmlAttributes));
        }

        public static MvcHtmlString TextBoxIntegerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, bool autoFormat = true, bool positive = false, string iconClass = null)
        {
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string tmpValue = null;
            if (metadata.Model != null) tmpValue = metadata.Model.ToInt().ToString("N0");

            return TextBoxBuilder("mlInputInteger mlTextRight", htmlHelper, metadata, fullHtmlFieldName, tmpValue, GetHtmlAttributesSafely(htmlAttributes), null, autoFormat, positive, iconClass);
        }

        public static MvcHtmlString TextBoxInteger(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes = null, bool autoFormat = true, bool positive = false, string iconClass = null)
        {
            string tmpValue = null;
            if (value != null) tmpValue = value.ToInt().ToString("N0");

            return TextBoxBuilder("mlInputInteger mlTextRight", htmlHelper, null, name, tmpValue, GetHtmlAttributesSafely(htmlAttributes), null, autoFormat, positive, iconClass);
        }

        #endregion

        private static MvcHtmlString TextBoxBuilder(string funcCssName, HtmlHelper htmlHelper, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes, int? decimalPlaces = null, bool autoFormat = false, bool positive = false, string iconClass = null)
        {
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            if (htmlAttributes.ContainsKey("name")) fullHtmlFieldName = htmlAttributes["name"].ToString();
            if (!htmlAttributes.ContainsKey("id")) htmlAttributes.Add("id", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(string.IsNullOrEmpty(htmlHelper.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix) ? fullHtmlFieldName : name));

            var extendClasses = funcCssName + (autoFormat ? " mlNumericAutoFormat" : "") + (positive ? " mlNumericPositive" : "");
            if (htmlAttributes.ContainsKey("class")) htmlAttributes["class"] = htmlAttributes["class"] + " " + extendClasses;
            else htmlAttributes.Add("class", extendClasses);

            if (decimalPlaces != null)
            {
                htmlAttributes.Remove("decimalplaces");
                htmlAttributes.Add("decimalplaces", decimalPlaces);
            }

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);

            if (value != null) tagBuilder.MergeAttribute("value", value.ToString());

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState))
            {
                if (modelState.Errors.Count > 0) tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullHtmlFieldName, metadata));

            if (!string.IsNullOrEmpty(iconClass))
            {
                return new MvcHtmlString(String.Format(@"<div class='input-icon'>
                                                            <i class='{0}'></i>
                                                            {1}
                                                        </div>", iconClass, tagBuilder.ToString(TagRenderMode.SelfClosing)));                
            }

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        private static MvcHtmlString HiddenBuilder(HtmlHelper htmlHelper, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            if (htmlAttributes.ContainsKey("name")) fullHtmlFieldName = htmlAttributes["name"].ToString();
            if (!htmlAttributes.ContainsKey("id")) htmlAttributes.Add("id", htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(string.IsNullOrEmpty(htmlHelper.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix) ? fullHtmlFieldName : name));

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
            tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);

            if (value != null) tagBuilder.MergeAttribute("value", value.ToString());

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState))
            {
                if (modelState.Errors.Count > 0) tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullHtmlFieldName, metadata));

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        private static Dictionary<string, object> GetHtmlAttributesSafely(object htmlAttributes = null)
        {
            return htmlAttributes != null ? new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)) : new Dictionary<string, object>();
        }

        private static void MergeClassAttribute(IDictionary<string, object> htmlAttributes, params string[] extendClasses)
        {
            if (extendClasses.Length == 0)
            {
                return;
            }

            var classes = new List<string>();

            if (htmlAttributes.ContainsKey("class"))
            {
                classes.Add(htmlAttributes["class"].ToStr());
                htmlAttributes.Remove("class");
            }

            classes.AddRange(extendClasses);

            htmlAttributes.Add("class", string.Join(" ", classes));
        }
    }
}
