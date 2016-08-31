using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Share.Helper.Models;

namespace Share.Helper.Extension
{
    public static class HtmlHelperPagination
	{
        public static MvcHtmlString PaginationFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TValue>> expression, 
            string pageClickFunction, 
            string pageSizeClickFunction,
            string templateName = "_Pagination")
        {
            var model = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model as PaginationModel;

            if (model != null)
            {
                model.PageClickFunction = pageClickFunction;
                model.PageSizeClickFunction = pageSizeClickFunction;

                model.CalculatePaging();
                var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

                return htmlHelper.EditorFor(expression, templateName, new { paginationId = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName) });
            }

            return MvcHtmlString.Create(string.Empty);
        }

        public static MvcHtmlString Pagination2For<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string pageClickFunction,
            string pageSizeClickFunction)
        {
            return htmlHelper.PaginationFor(expression, pageClickFunction, pageSizeClickFunction, "_Pagination2");
        }
	}
}