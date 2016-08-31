using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.BeUser;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using ML.Common;
using Site.Backend.Models;

namespace Site.Backend.Controllers
{
    public class ProfileController : AuthorisedController
    {
        public ProfileController() : base(BePage.Profile) { }

        public ActionResult Index()
        {
            var beUser = CurrentUser;
            if (beUser == null)
            {
                SendNotification(NotifyType.Error, BackendMessage.CannotLoadData);
                return RedirectToAction("Index", "Home");
            }

            var model = beUser.Map<BeUser, BeUserModel>();
            model.OldImagePath = model.ImagePath;

            return View(model);
        }

        public ActionResult Save(BeUserModel model)
        {
            if (!ModelState.IsValid && GetModelStateErrorList().Any())
            {
                return JsonObject(false, GetModelStateErrors());
            }

            if (model.Id != CurrentUserId)
            {
                return JsonObject(false, BackendMessage.BadRequest);
            }

            #region file hanlder

            if (Request.Files.Count > 0 && !string.IsNullOrWhiteSpace(Request.Files[0].FileName))
            {
                var file = Request.Files[0];

                var extension = Path.GetExtension(file.FileName).ToStr();

                if (!SiteUtils.IsImageFile(file.FileName))
                {
                    return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                }

                if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                {
                    return JsonObject(false, BackendMessage.FileMaxSize5MB);
                }

                if (!string.IsNullOrWhiteSpace(model.OldImagePath))
                {
                    DeleteImageFile(model.OldImagePath);
                }

                model.ImagePath = Guid.NewGuid() + extension;

                var filePath = PhysicalDataFilePath(model.ImagePath);
                file.SaveAs(filePath);
            }
            else if (string.IsNullOrWhiteSpace(model.ImagePath) && !string.IsNullOrWhiteSpace(model.OldImagePath))
            {
                DeleteImageFile(model.OldImagePath);
            }

            #endregion

            var request = new SaveRequest
            {
                Password = model.Password,
                IsUpdateProfile = true,
                Entity = model.Map<BeUserModel, BeUser>()
            };

            var res = ServiceHelper.BeUser.ExecuteDispose(s => s.SaveBeUser(request));

            if (res.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);
                return JsonObject(true, string.Empty);
            }

            var msg = res.Messages.FirstOrDefault();

            return JsonObject(false, string.IsNullOrWhiteSpace(msg) ? BackendMessage.ErrorOccure : msg.GetBeUserRes());
        }

        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        public ActionResult SaveChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return JsonObject(false, GetModelStateErrors());
            }

            var response = ServiceHelper.BeUser.ExecuteDispose(s => s.SaveChangePassword(new ChangePwdResquest
            {
                UserId = CurrentUserId,
                Password = model.OldPassword,
                NewPassword = model.NewPassword
            }));

            if (response.Success)
            {
                return JsonObject(true, Share.Messages.BeScreens.Profile.Profile.ChangePasswordSuccess);
            }

            var msg = response.Messages.FirstOrDefault().ToStr().GetServiceMessageRes();

            return JsonObject(false, msg);
        }
    }
}