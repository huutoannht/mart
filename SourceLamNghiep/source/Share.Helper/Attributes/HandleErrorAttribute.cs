using System;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Data.DataContract;
using Share.Messages;
using ML.Common.Error;
using ML.Common.Log;

namespace Share.Helper.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class HandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        private readonly ILogger _log = LogManager.GetLogger();

        public override void OnException(ExceptionContext exceptionContext)
        {
            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.Clear();

            string errorMessage;

            //FaultException, CommunicationException already logged in Service layer

            if (exceptionContext.Exception is FaultException<ErrorManager>)
            {
                var faultException = exceptionContext.Exception as FaultException<ErrorManager>;
                errorMessage = faultException.Detail.Description;
            }
            else
            {
                var original = exceptionContext.Exception.InnerException ?? exceptionContext.Exception;
                errorMessage = original.Message;

                if (!(exceptionContext.Exception is CommunicationException))
                {
                    LogExceptionDetails(exceptionContext);
                }
            }

            if (exceptionContext.HttpContext.Request.IsAjaxRequest())
            {
                var dObj = SiteUtils.JsonObjectMergeProperties(false, BackendMessage.ErrorOccure, null);

                var result = new JsonResult
                {
                    Data = dObj.ToDictionary(d => d.Key, d => d.Value)
                };

                exceptionContext.Result = result;

                return;
            }

            exceptionContext.Controller.TempData[NotifyType.Error.ToString()] = HttpUtility.UrlDecode(errorMessage);
            exceptionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Index", area = string.Empty }));
        }

        private void LogExceptionDetails(ExceptionContext exceptionContext)
        {
            var builder = new StringBuilder();
            builder.Append("Unhandled exception from ");

            builder.AppendFormat(" {0}", exceptionContext.HttpContext.Request.UserHostAddress);
            string forwardedFor = exceptionContext.HttpContext.Request.Headers.Get("X-Forwarded-For");
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                builder.AppendFormat(" ({0})", forwardedFor);
            }

            builder.AppendFormat(" to: {0}", exceptionContext.HttpContext.Request.RawUrl);
            builder.AppendFormat(
                    " - {0}/{1} {2}",
                    exceptionContext.Controller.ControllerContext.RouteData.Values["controller"],
                    exceptionContext.Controller.ControllerContext.RouteData.Values["action"],
                    exceptionContext.HttpContext.Request.HttpMethod);

            builder.Append(Environment.NewLine);
            builder.Append("-------------");
            builder.Append(Environment.NewLine);
            builder.Append(exceptionContext.Exception);

            _log.Error(builder.ToString(), exceptionContext.Exception);
        }
    }
}