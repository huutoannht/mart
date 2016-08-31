using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using Data.DataContract;
using ML.Common;
using Share.Helper.Models;

namespace Share.Helper.Extension
{
	public static class HtmlHelperExt
	{
	    public static bool IsItemSavedInCookie(this HtmlHelper htmlHelper, string key, string value, string seperator = ";")
	    {
	        var request = htmlHelper.ViewContext.HttpContext.Request;

	        if (request.Cookies[key] == null) return false;
	        var cookieVal = request.Cookies[key].Value;

	        if (string.IsNullOrWhiteSpace(cookieVal)) return false;

	        cookieVal = htmlHelper.ViewContext.HttpContext.Server.UrlDecode(cookieVal);

	        var arr = cookieVal.Split(new[] {seperator}, StringSplitOptions.RemoveEmptyEntries).ToList();

	        return arr.Contains(value);
	    }

        public static bool ImageIsExist(this HtmlHelper htmlHelper, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var fullPath = htmlHelper.ViewContext.HttpContext.Server.MapPath(Path.Combine(ConstKeys.DataImageFolder, fileName));

            return File.Exists(fullPath);
        }

        public static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

		public static void EnablePartialViewValidation(this HtmlHelper helper)
		{
			if (helper.ViewContext.FormContext == null)
			{
				helper.ViewContext.FormContext = new FormContext();
			}
		}

		public static string GetFullHtmlFieldId<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			var htmlField = ExpressionHelper.GetExpressionText(expression);
			var htmlFieldId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlField);

			return htmlFieldId;
		}

		public static string GetFullHtmlFieldName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			var htmlField = ExpressionHelper.GetExpressionText(expression);
			var htmlFieldId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlField);

			return htmlFieldId;
		}

		public static MvcHtmlString HiddenIf(this HtmlHelper htmlHelper, bool renderIfTrue, string name)
		{
			return renderIfTrue ? htmlHelper.Hidden(name) : MvcHtmlString.Empty;
		}

		public static MvcHtmlString HiddenIf(this HtmlHelper htmlHelper, bool renderIfTrue, string name, object value)
		{
			return renderIfTrue ? htmlHelper.Hidden(name, value) : MvcHtmlString.Empty;
		}

        public static string EvalString(this HtmlHelper htmlHelper, string key, string format)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        public static bool EvalBoolean(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToBoolean(htmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
        }

		public static IHtmlString RenderBundleScriptIf(this HtmlHelper htmlHelper, bool renderIfTrue, params  string[] paths)
        {
            return renderIfTrue ? Scripts.Render(paths) : MvcHtmlString.Empty;
        }

        public static Dictionary<string, object> GetHtmlAttributes(object htmlAttributes = null)
        {
            return htmlAttributes != null ? new Dictionary<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)) : new Dictionary<string, object>();
        }

        public static void MergeAttributes(IDictionary<string, object> htmlAttributes, string attributeName, string attributeValue)
        {
            if (string.IsNullOrWhiteSpace(attributeName) || string.IsNullOrWhiteSpace(attributeValue))
            {
                return;
            }

            var classes = new List<string>();

            if (htmlAttributes.ContainsKey(attributeName))
            {
                classes.Add(htmlAttributes[attributeName].ToStr());
                htmlAttributes.Remove(attributeName);
            }

            classes.Add(attributeValue);

            htmlAttributes.Add(attributeName, string.Join(" ", classes));
        }

        public static string Area(this HtmlHelper helper)
        {
            return (string)helper.ViewContext.RouteData.DataTokens["area"] ?? "";
        }

        public static string IsMenuSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "active";
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static bool ThisPageIs(this HtmlHelper html, string controller = null, string action = null)
        {
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction;
        }

	    public static string ClassSort(this HtmlHelper html, string colName, string sortBy, SortDirection sortDirection)
	    {
            if (colName != sortBy) return  "sorting";

            return sortDirection == SortDirection.Asc ? "sorting_asc" : "sorting_desc";
	    }

        public static string FilterSlideControlClass(this HtmlHelper html, bool filterVisible)
	    {
            return filterVisible ? "fa-chevron-up slide-control-filter" : "fa-chevron-down slide-control-filter";
	    }

        public static string FilterBodyClass(this HtmlHelper html, bool filterVisible)
        {
            return filterVisible ? string.Empty : "hide-noneim";
        }
	}
}
