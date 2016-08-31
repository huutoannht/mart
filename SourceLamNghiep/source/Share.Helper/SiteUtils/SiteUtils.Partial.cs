using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Share.Helper
{
    public static partial class SiteUtils
    {
        public static string PartialToString(string viewName, object model, ViewContext viewContext)
        {
            if (String.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("viewName is empty");
            }

            var context = new ControllerContext(viewContext.RequestContext, viewContext.Controller);
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);

                var newViewContext = new ViewContext(context, viewResult.View, viewContext.ViewData, viewContext.TempData, sw) { ViewData = { Model = model } };
                viewResult.View.Render(newViewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string PartialToString(this HtmlHelper html, string viewName, object model)
        {
            return PartialToString(html.ViewContext.Controller, viewName, model);
        }

        public static string PartialToString(this ControllerBase controllerBase, string viewName, object model, ViewDataDictionary viewData = null)
        {
            controllerBase.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerBase.ControllerContext, viewName);
                var viewContext = new ViewContext(controllerBase.ControllerContext, viewResult.View, viewData ?? controllerBase.ViewData, controllerBase.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerBase.ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
