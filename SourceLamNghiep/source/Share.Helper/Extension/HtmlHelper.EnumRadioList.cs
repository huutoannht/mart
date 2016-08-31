using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Share.Helper.Extension;

namespace Share.Helper.Extension
{
    public static class HtmlHelperEnumRadioList
    {
        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumRadioFor(htmlHelper, expression, null, null, null, null);
        }

        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumRadioFor(htmlHelper, expression, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToExclude)
        {
            return EnumRadioFor(htmlHelper, expression, null, null, null, enumValuesToExclude);
        }

        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToExclude, string optionLabel, object htmlAttributes)
        {
            return EnumRadioFor(htmlHelper, expression, htmlAttributes, null, optionLabel, enumValuesToExclude);
        }

        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IEnumerable<TEnum> enumValuesToInclude, string optionLabel)
        {
            return EnumRadioFor(htmlHelper, expression, null, enumValuesToInclude, optionLabel, null);
        }

        public static MvcHtmlString EnumRadioFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes, IEnumerable<TEnum> enumValuesToInclude, string optionLabel, IEnumerable<TEnum> enumValueToExclude)
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
                    Text = e.GetDescription(false),
                    Value = e.ToString(),
                    Selected = e.Equals(metadata.Model)
                }).ToList();

            if (optionLabel != null)
            {
                new[] { new SelectListItem { Text = optionLabel } }.Concat(items);
            }

            var html = new StringBuilder();
            var name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
            for (var i = 0; i < items.Count; i++)
            {
                html.AppendLine(string.Format(@"
                    <div class=""radio-custom radio-inline"">
                        {0}
                        <label for=""{1}"">{2}</label>
                    </div>", htmlHelper.RadioButton(name, items[i].Value, i == 0, new { id = items[i].Value }), items[i].Value, items[i].Text));
            }
            return new MvcHtmlString(html.ToString());
        }
    }
}