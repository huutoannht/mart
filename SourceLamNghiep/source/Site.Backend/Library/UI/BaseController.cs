using System.Text;
using System.Web.Script.Serialization;
using Share.Helper.UI;
using Site.Backend.Library.Helper;
using Site.Backend.Library.Session;
using Data.DataContract;
using Share.Helper;
using Share.Helper.Cache;
using Share.Helper.Client;
using ML.Common;
using ML.Common.Log;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Site.Backend.Library.UI
{
    [Share.Helper.Attributes.HandleError]
    [ValidateInput(false)]
    public abstract partial class BaseController : CommonBaseController
    {
        #region Constructor

        private readonly IContainer _container;

        protected readonly ILogger Log;

        protected BaseController()
        {
            _container = ObjectFactory.GetInstance<IContainer>();

            Log = LogManager.GetLogger(GetType());

        }

        #endregion

        #region Helper

        private IServiceHelper _serviceHelper;

        private IAuthenHelper _authenHelper;

        private ICacheHelper _cacheHelper;

        private ISessionHelper _sessionHelper;

        private IDateTimeHelper _dateTimeHelper;

        protected IAuthenHelper AuthenHelper
        {
            get { return _authenHelper ?? (_authenHelper = ObjectFactory.GetInstance<IAuthenHelper>()); }
        }

        protected IServiceHelper ServiceHelper
        {
            get { return _serviceHelper ?? (_serviceHelper = ObjectFactory.GetInstance<IServiceHelper>()); }
        }

        protected ICacheHelper CacheHelper
        {
            get { return _cacheHelper ?? (_cacheHelper = ObjectFactory.GetInstance<ICacheHelper>()); }
        }

        protected ISessionHelper SessionHelper
        {
            get { return _sessionHelper ?? (_sessionHelper = ObjectFactory.GetInstance<ISessionHelper>()); }
        }

        protected IDateTimeHelper DateTimeHelper
        {
            get { return _dateTimeHelper ?? (_dateTimeHelper = _container.GetInstance<IDateTimeHelper>()); }
        }

        #endregion

        #region Methods

        protected string GetModelStateErrors()
        {
            return "<ul>" + string.Join(string.Empty, GetModelStateErrorList()) + "</ul>";
        }

        protected List<string> GetModelStateErrorList()
        {
            var listError = new List<string>();

            foreach (var modelState in ModelState.Values)
            {
                if (modelState.Errors.Any(i => !string.IsNullOrWhiteSpace(i.ErrorMessage)))
                {
                    listError.AddRange(modelState.Errors.Select(error => "<li>" + error.ErrorMessage + "</li>"));
                }
            }

            return listError;
        }

        protected string GetMessageFromList(List<string> listMsg)
        {
            var sb = new StringBuilder();
            sb.Append("<ul>");
            listMsg.ForEach(msg => sb.Append(string.Format("<li>{0}</li>", msg)));
            sb.Append("</ul>");
            return sb.ToString();
        }

        #endregion

        #region Partial

        protected string PartialViewToString()
        {
            return PartialViewToString(null, null);
        }

        protected string PartialViewToString(string viewName)
        {
            return PartialViewToString(viewName, null);
        }

        protected string PartialViewToString(object model)
        {
            return PartialViewToString(null, model);
        }

        protected string PartialViewToString(string viewName, object model)
        {
            return this.PartialToString(viewName, model);
        }

        protected string PartialViewToString(string viewName, object model, ViewDataDictionary viewData)
        {
            return this.PartialToString(viewName, model, viewData);
        }

        #endregion

        #region Json Object

        protected JsonResult JsonObject(bool success, string message, object data = null, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            var dObj = SiteUtils.JsonObjectMergeProperties(success, message, data);

            return Json(dObj.ToDictionary(d => d.Key, d => d.Value), behavior);
        }

        #endregion

        #region Notification

        protected void SendNotification(NotifyType type, string message)
        {
            TempData[ConstKeys.HasNotification] = true;
            TempData[type.ToStr()] = message;
        }

        #endregion

        protected void PopulateViewData(string key, object data)
        {
            if (ViewData[key] == null)
            {
                ViewData[key] = data;
            }
        }

        protected string ObjectToLog(object obj)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(obj);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            UpdateCurrentUserTimeZone(filterContext);
        }

        private void UpdateCurrentUserTimeZone(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(SessionHelper.CurrentTimeZone))
            {
                var key = ConstKeys.BrowserTimeZone;
                if (filterContext.HttpContext.Request.Cookies.AllKeys.Contains(key))
                {
                    var httpCookie = filterContext.HttpContext.Request.Cookies[key];
                    if (httpCookie != null && !string.IsNullOrWhiteSpace(httpCookie.Value))
                    {
                        var timezoneOffset = Server.UrlDecode(httpCookie.Value);

                        if (timezoneOffset == "00:00")
                        {
                            timezoneOffset = "(UTC)";
                        }

                        var value = string.Empty;

                        foreach (var info in TimeZoneInfo.GetSystemTimeZones())
                        {
                            if (info.DisplayName.Contains(timezoneOffset))
                            {
                                value = info.Id;
                                break;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            SessionHelper.CurrentTimeZone = Server.UrlDecode(value);
                        }
                    }
                }
            }
        }
    }
}
