using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Models.AppSetting;
using Share.Helper.Extension;
using Site.Backend.Library.Attribute;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Share.Helper;
using Share.Messages;
using ML.Common;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class LogoController : AuthorisedController
    {
        public LogoController() : base(BePage.Logo) { }

        [View]
        public ActionResult Index()
        {
            //Logo top
            var appSetting =
                CacheHelper.GetAppSettings(AppSettingType.SiteData)
                    .FirstOrDefault(i => i.Name == ConstKeys.SiteLogoImagePath)
                    ?? new AppSetting
                    {
                        SettingType = AppSettingType.SiteData,
                        Name = ConstKeys.SiteLogoImagePath
                    };

            var model = appSetting.Map<AppSetting, AppSettingModel>();

            model.ImagePath = model.Value;
            model.OldImagePath = model.Value;

            //----logo footer

            appSetting =
                CacheHelper.GetAppSettings(AppSettingType.SiteData)
                    .FirstOrDefault(i => i.Name == ConstKeys.SiteLogoImagePath2)
                    ?? new AppSetting
                    {
                        SettingType = AppSettingType.SiteData,
                        Name = ConstKeys.SiteLogoImagePath2
                    };

            model.Id2 = appSetting.Id;
            model.Name2 = appSetting.Name;
            model.ImagePath2 = appSetting.Value;
            model.OldImagePath2 = appSetting.Value;

            //--- Browser icon
            appSetting =
                CacheHelper.GetAppSettings(AppSettingType.SiteData)
                    .FirstOrDefault(i => i.Name == ConstKeys.SiteLogoImagePath3)
                    ?? new AppSetting
                    {
                        SettingType = AppSettingType.SiteData,
                        Name = ConstKeys.SiteLogoImagePath3
                    };

            model.Id3 = appSetting.Id;
            model.Name3 = appSetting.Name;
            model.ImagePath3 = appSetting.Value;
            model.OldImagePath3 = appSetting.Value;


            //-----

            model.ControllerReturn = "Logo";
            model.ActionReturn = "Index";

            model.ControllerSave = "Logo";
            model.ActionSave = "SaveImage";

            return View(model);
        }

        [Update]
        public ActionResult SaveImage(AppSettingModel model)
        {
            var controller = model.ControllerReturn;
            var action = model.ActionReturn;

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                SendNotification(NotifyType.Error, Share.Messages.BeScreens.SiteSetting.SiteSetting.NameRequired);
                return RedirectToAction(action, controller);
            }

            #region file hanlder

            if (Request.Files["fileImage"] != null && !string.IsNullOrWhiteSpace(Request.Files["fileImage"].FileName))
            {
                var file = Request.Files["fileImage"];

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

            if (Request.Files["fileImage2"] != null && !string.IsNullOrWhiteSpace(Request.Files["fileImage2"].FileName))
            {
                var file = Request.Files["fileImage2"];

                var extension = Path.GetExtension(file.FileName).ToStr();

                if (!SiteUtils.IsImageFile(file.FileName))
                {
                    return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                }

                if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                {
                    return JsonObject(false, BackendMessage.FileMaxSize5MB);
                }

                if (!string.IsNullOrWhiteSpace(model.OldImagePath2))
                {
                    DeleteImageFile(model.OldImagePath2);
                }

                model.ImagePath2 = Guid.NewGuid() + extension;

                var filePath = PhysicalDataFilePath(model.ImagePath2);
                file.SaveAs(filePath);
            }
            else if (string.IsNullOrWhiteSpace(model.ImagePath2) && !string.IsNullOrWhiteSpace(model.OldImagePath2))
            {
                DeleteImageFile(model.OldImagePath2);
            }

            if (Request.Files["fileImage3"] != null && !string.IsNullOrWhiteSpace(Request.Files["fileImage3"].FileName))
            {
                var file = Request.Files["fileImage3"];

                var extension = Path.GetExtension(file.FileName).ToStr();

                if (!SiteUtils.IsImageFile(file.FileName))
                {
                    return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                }

                if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                {
                    return JsonObject(false, BackendMessage.FileMaxSize5MB);
                }

                if (!string.IsNullOrWhiteSpace(model.OldImagePath3))
                {
                    DeleteImageFile(model.OldImagePath3);
                }

                model.ImagePath3 = Guid.NewGuid() + extension;

                var filePath = PhysicalDataFilePath(model.ImagePath3);
                file.SaveAs(filePath);
            }
            else if (string.IsNullOrWhiteSpace(model.ImagePath3) && !string.IsNullOrWhiteSpace(model.OldImagePath3))
            {
                DeleteImageFile(model.OldImagePath3);
            }

            #endregion

            var appSetting = model.Map<AppSettingModel, AppSetting>();
            appSetting.Value = model.ImagePath;
            appSetting.Name = appSetting.Name.ToStr().Trim();

            var result = ServiceHelper.AppSetting.ExecuteDispose(s => s.SaveAppSetting(new SaveRequest
            {
                Entity = appSetting
            }));

            if (!result.Success)
            {
                return JsonObject(false, result.Messages.FirstOrDefault().GetServiceMessageRes());
            }

            if (!string.IsNullOrWhiteSpace(model.Name2))
            {
                appSetting.Id = model.Id2;
                appSetting.Value = model.ImagePath2;
                appSetting.Name = model.Name2.ToStr().Trim();

                result = ServiceHelper.AppSetting.ExecuteDispose(s => s.SaveAppSetting(new SaveRequest
                {
                    Entity = appSetting
                }));

                if (!result.Success)
                {
                    return JsonObject(false, result.Messages.FirstOrDefault().GetServiceMessageRes());
                }
            }

            if (!string.IsNullOrWhiteSpace(model.Name3))
            {
                appSetting.Id = model.Id3;
                appSetting.Value = model.ImagePath3;
                appSetting.Name = model.Name3.ToStr().Trim();

                result = ServiceHelper.AppSetting.ExecuteDispose(s => s.SaveAppSetting(new SaveRequest
                {
                    Entity = appSetting
                }));

                if (!result.Success)
                {
                    return JsonObject(false, result.Messages.FirstOrDefault().GetServiceMessageRes());
                }
            }

            foreach (AppSettingType type in Enum.GetValues(typeof(AppSettingType)))
            {
                CacheHelper.ClearGetAppSettings(type);
            }

            SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);

            return JsonObject(true, string.Empty);
        }
    }
}