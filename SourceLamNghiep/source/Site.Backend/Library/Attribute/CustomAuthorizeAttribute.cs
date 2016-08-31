using System.Linq;
using System.Web.Mvc;
using Share.Helper;
using Share.Messages;

namespace Site.Backend.Library.Attribute
{
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var dObj = SiteUtils.JsonObjectMergeProperties(false, BackendMessage.SessionTimeOut, null);

                var result = new JsonResult
                {
                    Data = dObj.ToDictionary(d => d.Key, d => d.Value)
                };

                filterContext.Result = result;
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}