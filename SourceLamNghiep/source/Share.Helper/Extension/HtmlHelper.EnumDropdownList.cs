using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Share.Helper.Extension;

namespace Share.Helper.Extension
{
    public static class HtmlHelperEnumDropdownList
    {
        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownFor(htmlHelper, expression, null, null, null, null);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, optionLabel, null);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToExclude)
        {
            return EnumDropDownFor(htmlHelper, expression, null, null, null, enumValuesToExclude);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToExclude, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, optionLabel, enumValuesToExclude);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToExclude, string optionLabel, object htmlAttributes, bool friendlyCase)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, optionLabel, enumValuesToExclude, friendlyCase);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToInclude, string optionLabel)
        {
            return EnumDropDownFor(htmlHelper, expression, null, enumValuesToInclude, optionLabel, null);
        }

        public static MvcHtmlString EnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes, IEnumerable<TEnum> enumValuesToInclude, string optionLabel, IEnumerable<TEnum> enumValueToExclude, bool friendlyCase = true)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var enumType = Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum);

            IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();

            if (enumValueToExclude != null)
            {
                enumValues = enumValues.Except(enumValueToExclude);
            }

            if (enumValuesToInclude != null)
            {
                enumValues = enumValues.Where(enumValuesToInclude.Contains);
            }

            var items = enumValues
                .Select(e => new SelectListItem
                                 {
                                     Text = e.GetDescription(false).GetEnumDesRes(),//do not change, because : this system must be localization
                                     Value = e.ToString(),
                                     Selected = e.Equals(metadata.Model)
                                 }).ToList();

            if (optionLabel != null)
            {
                new[] { new SelectListItem { Text = optionLabel } }.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
        }
    }
}