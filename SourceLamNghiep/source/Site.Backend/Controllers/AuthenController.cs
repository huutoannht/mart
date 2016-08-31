using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.Authen;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.RequestPasswordDC;
using Data.DataContract.BeUserDC;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages.BeScreens.Login;
using ML.Common;

namespace Site.Backend.Controllers
{
    public class AuthenController : BaseController
    {
        public ActionResult Index()
        {
            AuthenHelper.Logout();
            var model = new AuthenViewModel
            {
                ReturnUrl = Request["ReturnUrl"],
                Remember = true
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ValidateUser(AuthenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = ServiceHelper.BeUser.ExecuteDispose(s => s.CheckLogin(new CheckLoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                }));

                if (result.Messages.Any())
                {
                    return JsonObject(false, result.Messages.FirstOrDefault().ToStr().GetServiceMessageRes());
                }

                CacheHelper.ClearGetBeUser(result.BeUser.Id);

                SessionHelper.CurrentUserId = result.BeUser.Id;

                FormsAuthentication.SetAuthCookie(SessionHelper.CurrentUserId.ToStr(), model.Remember);

                Session[ConstKeys.SESSION_PATH_KEY] = Url.Content(ConstKeys.DataImageFolder);

                return JsonObject(true, string.Empty, new
                {
                    defaultUrl = string.IsNullOrWhiteSpace(model.ReturnUrl) ? Url.Action("Index", "Home") : model.ReturnUrl
                });
            }

            return JsonObject(false, GetModelStateErrors());
        }

        public ActionResult ForgotPassword()
        {
            var model = new RecoverPasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitRequestPassword(RecoverPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new SubmitPasswordRequest
                {
                    IsBeUser = true,
                    Email = model.Email,
                    Link = Url.Action("RecoverPassword", "Authen", new { }, Request.Url.Scheme)
                };

                var response = ServiceHelper.RequestPassword.ExecuteDispose(s => s.SubmitRequestPassword(request));

                if (response.Success)
                {
                    SendNotification(NotifyType.Success, SiteUtils.GetMessage(MessageCase.ForgotPasswordEmailSent));
                    return JsonObject(true, string.Empty);
                }

                return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
            }

            return JsonObject(false, GetModelStateErrors());
        }

        public ActionResult RecoverPassword(string id)
        {
            var requestData = ServiceHelper.RequestPassword.ExecuteDispose(s => s.GetByRefKey(id));

            if (requestData == null)
            {
                SendNotification(NotifyType.Error, Share.Messages.BeScreens.RecoverPassword.RecoverPassword.BadRequest);
                return RedirectToAction("Index", "Error");
            }

            var model = new ResetPasswordModel
            {
                KeyRef = requestData.KeyRef
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveNewPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var response = ServiceHelper.BeUser.ExecuteDispose(s => s.ChangePwd(new ChangePwdResquest
                {
                    KeyRef = model.KeyRef,
                    NewPassword = model.NewPassword
                }));

                if (response.Success)
                {
                    SendNotification(NotifyType.Success, Login.ChangePassSuccess);
                    return JsonObject(true, string.Empty, new
                    {
                        loginUrl = Url.Action("Index")
                    });
                }

                var msg = response.Messages.FirstOrDefault().ToStr().GetServiceMessageRes();

                return JsonObject(false, msg);
            }

            return JsonObject(false, GetModelStateErrors());
        }
    }
}
