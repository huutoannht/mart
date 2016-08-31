using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ML.Common;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Models.BeUser;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Share.Messages.BeScreens.Administrator;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class AdministratorController : AuthorisedController
    {
        public AdministratorController() : base(BePage.BackendUsers) { }

        [View]
        public ActionResult Index(BeUserIndexModel model)
        {
            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "FullName";
            }

            var filter = new FindRequest
            {
                TextSearch = model.TextSearch,
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
            };

            var response = ServiceHelper.BeUser.ExecuteDispose(s => s.FindBeUsers(filter));
            model.Results = response.Results.MapList<BeUserModel>();
            model.Pagination.TotalRecords = response.TotalRecords;

            return View(model);
        }

        [View]
        public ActionResult Add()
        {
            var model = new BeUserModel
            {
                Type = BeUserType.Admin,
                Status = BeUserStatus.Active
            };
            return View("Edit", model);
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            var beUser = ServiceHelper.BeUser.ExecuteDispose(s => s.GetBeUser(id));

            if (beUser == null)
            {
                SendNotification(NotifyType.Error, BackendMessage.CannotLoadData);
                return RedirectToAction("Index");
            }

            var model = beUser.Map<BeUser, BeUserModel>();
            model.OldImagePath = model.ImagePath;

            return View("Edit", model);
        }

        public ActionResult Save(BeUserModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (ModelState.IsValid)
            {
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

                #endregion

                var request = new SaveRequest
                {
                    Password = model.Password,
                    Entity = model.Map<BeUserModel, BeUser>()
                };

                var res = ServiceHelper.BeUser.ExecuteDispose(s => s.SaveBeUser(request));

                if (res.Success)
                {
                    CacheHelper.ClearGetBeUser(res.EntityId);
                    SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);

                    #region send mail

                    if (model.IsNew)
                    {
                        var emailTemplate = CacheHelper.GetSystemEmailTemplates()
                                .FirstOrDefault(i => i.Type == SystemEmailTemplateType.ConfirmNewAccountToUser
                                && i.Language == SiteUtils.GetDefaultLanguageIfNullOrEmpty());
                        if (emailTemplate != null)
                        {
                            var mapKeys = new Dictionary<string, string>();

                            EmailUtils.Instance.SendEmail(model.Email, emailTemplate, mapKeys);
                        }
                    }

                    #endregion

                    return JsonObject(true, string.Empty);
                }

                var msg = res.Messages.FirstOrDefault();

                return JsonObject(false, string.IsNullOrWhiteSpace(msg) ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
            }

            return JsonObject(false, GetModelStateErrors());
        }

        [Delete]
        public ActionResult Delete(Guid id)
        {
            if (id == CurrentUserId)
            {
                return JsonObject(false, Administrator.CannotDeleteYourSelf);
            }

            var response = ServiceHelper.BeUser.ExecuteDispose(s => s.Delete(id, PhysicalDataImagesFolderPath));
            if (response.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.DeleteSuccessfull);
                return JsonObject(true, string.Empty);
            }

            var msg = response.Messages.FirstOrDefault();
            return JsonObject(false, string.IsNullOrWhiteSpace(msg)
                ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
        }
    }
}