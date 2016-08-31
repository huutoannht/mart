using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using ML.Common;
using Share.Helper;
using Site.Backend.Library.UI;
using Share.Messages;

namespace Site.Backend.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            if (TempData[NotifyType.Error.ToString()].ToStr() == BackendMessage.YouLostSession || string.IsNullOrWhiteSpace(TempData[NotifyType.Error.ToString()].ToStr()))
            {
                TempData[ConstKeys.HasNotification] = null;
                TempData[NotifyType.Error.ToString()] = null;
                TempData[NotifyType.Warning.ToString()] = null;
                TempData[NotifyType.Information.ToString()] = null;
                TempData[NotifyType.Success.ToString()] = null;
                return RedirectToAction("Index", "Authen");
            }

            ViewBag.Title = BackendMessage.Error;
            return View();
        }

        public ActionResult AccessDenied()
        {
            ViewBag.Title = BackendMessage.AccessDined;
            return View("Index");
        }
    }
}