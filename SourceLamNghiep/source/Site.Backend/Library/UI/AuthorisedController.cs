using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Share.Messages;
using Site.Backend.Library.Attribute;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Share.Helper;
using Share.Helper.Extension;
using ML.Common;

namespace Site.Backend.Library.UI
{
    [CustomAuthorize]
    public class AuthorisedController : BaseController
    {
        private readonly BePage _page;

        public AuthorisedController(BePage page)
        {
            _page = page;
        }

        protected Guid CurrentUserId { get { return SessionHelper.CurrentUserId; } }

        protected BeUser CurrentUser { get { return SessionHelper.CurrentUser; } }

        protected string CurrentUserTimeZoneId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CurrentUser.TimeZoneId))
                    return CurrentUser.TimeZoneId;

                var cookie = Request.Cookies[ConstKeys.BrowserTimeZone];

                if (cookie != null)
                {
                    return cookie.Value.GetServerTimeZoneId();
                }

                return string.Empty.GetServerTimeZoneId();
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CurrentUserTimeZoneId = CurrentUserTimeZoneId;
            base.OnActionExecuting(filterContext);
        }

        protected DateTime? DateFrom(DateTime? dateTimeFrom)
        {
            return DateTimeHelper.ConvertToUtcNull(dateTimeFrom, CurrentUserTimeZoneId).ToStartDateTimeNull();
        }

        protected DateTime? DateTo(DateTime? dateTimeTo)
        {
            return DateTimeHelper.ConvertToUtcNull(dateTimeTo, CurrentUserTimeZoneId).ToEndDateTimeNull();
        }

        #region permissions

        public bool CanView
        {
            get { return SessionHelper.CanView(_page); }
        }

        public bool CanAdd
        {
            get { return SessionHelper.CanAdd(_page); }
        }

        public bool CanUpdate
        {
            get { return SessionHelper.CanUpdate(_page); }
        }

        public bool CanDelete
        {
            get { return SessionHelper.CanDelete(_page); }
        }

        public bool CanImport
        {
            get { return SessionHelper.CanImport(_page); }
        }

        public bool CanExport
        {
            get { return SessionHelper.CanExport(_page); }
        }

        public ActionResult GetViewDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.ViewDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.ViewDenied);
        }

        public ActionResult GetAddDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.AddDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.AddDenied);
        }

        public ActionResult GetUpdateDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.UpdateDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.UpdateDenied);
        }

        public ActionResult GetDeleteDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.DeleteDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.DeleteDenied);
        }

        public ActionResult GetImportDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.ImportDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.ImportDenied);
        }

        public ActionResult GetExportDeniedResult()
        {
            if (Request.IsAjaxRequest())
            {
                return DeniedAjaxResult(BackendMessage.ExportDenied);
            }

            return CurrentUserId == Guid.Empty ? RedirectToLoginResult : RedirectToErrorPageResult(BackendMessage.ExportDenied);
        }

        protected ActionResult DeniedAjaxResult(string message)
        {
            var dObj = SiteUtils.JsonObjectMergeProperties(false, message, null);

            var result = new JsonResult
            {
                Data = dObj.ToDictionary(d => d.Key, d => d.Value)
            };

            return result;
        }

        protected ActionResult RedirectToLoginResult
        {
            get
            {
                return new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Authen"},
                            {"Action", "Index"},
                            {"Area", string.Empty}
                        });
            }
        }

        protected ActionResult RedirectToErrorPageResult(string message)
        {
            TempData[ConstKeys.HasNotification] = true;
            TempData[NotifyType.Error.ToString()] = message;
            return new RedirectToRouteResult(
                new RouteValueDictionary {{ "Controller", "Error" },
                        { "Action", "Index" }, { "Area", string.Empty } });
        }

        #endregion
    }
}