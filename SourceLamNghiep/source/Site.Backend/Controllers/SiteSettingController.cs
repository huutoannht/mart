using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models.AppSetting;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using ML.Common;

namespace Site.Backend.Controllers
{
    public class SiteSettingController : AuthorisedController
    {
        public SiteSettingController() : base(BePage.SiteSettings) { }

        [View]
        public ActionResult Index(AppSettingIndexModel model)
        {
            if (!model.SettingType.HasValue && !Request.IsPostAction())
            {
                model.SettingType = AppSettingType.Smtp;
            }

            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "Name";
            }

            var filter = new FindRequest
            {
                SettingType = model.SettingType,
                ExcludeSettingTypes = new List<AppSettingType> { AppSettingType.State },
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
            };

            var response = ServiceHelper.AppSetting.ExecuteDispose(s => s.FindAppSettings(filter));
            model.Results = response.Results.MapList<AppSettingModel>();
            model.Pagination.TotalRecords = response.TotalRecords;

            return View(model);
        }

        [View]
        public ActionResult AddNew(AppSettingType? settingType)
        {
            var model = new AppSettingModel();

            if (settingType.HasValue)
            {
                model.SettingType = settingType.Value;
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model)
            });
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            var model = ServiceHelper.AppSetting.ExecuteDispose(s => s.GetAppSetting(id)).Map<AppSetting, AppSettingModel>();

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model)
            });
        }

        public ActionResult Save(AppSettingModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (!ModelState.IsValid) return JsonObject(false, GetModelStateErrors());

            var appSetting = model.Map<AppSettingModel, AppSetting>();
            appSetting.Name = appSetting.Name.ToStr().Trim();

            var result = ServiceHelper.AppSetting.ExecuteDispose(s => s.SaveAppSetting(new SaveRequest
            {
                Entity = appSetting
            }));

            if (!result.Success)
            {
                return JsonObject(false, result.Messages.FirstOrDefault().GetServiceMessageRes());
            }

            foreach (AppSettingType type in Enum.GetValues(typeof(AppSettingType)))
            {
                CacheHelper.ClearGetAppSettings(type);
            }

            SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);

            return JsonObject(true, string.Empty);
        }

        [Delete]
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var success = ServiceHelper.AppSetting.ExecuteDispose(s => s.Delete(id));
            if (success)
            {
                foreach (AppSettingType type in Enum.GetValues(typeof(AppSettingType)))
                {
                    CacheHelper.ClearGetAppSettings(type);
                }

                SendNotification(NotifyType.Success, BackendMessage.DeleteSuccessfull);

                return JsonObject(true, string.Empty);
            }

            return JsonObject(false, BackendMessage.ErrorOccure);
        }
    }
}