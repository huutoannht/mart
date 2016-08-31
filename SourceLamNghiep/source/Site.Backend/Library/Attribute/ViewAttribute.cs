using System;
using System.Web.Mvc;
using Site.Backend.Library.UI;
using Share.Messages;

namespace Site.Backend.Library.Attribute
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as AuthorisedController;

            if (controller == null)
            {
                throw new Exception(BackendMessage.InheriteFromAuthorisedControllerClassMsg);
            }

            if (controller.CanView)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            filterContext.Result = controller.GetViewDeniedResult();
        }
    }
}