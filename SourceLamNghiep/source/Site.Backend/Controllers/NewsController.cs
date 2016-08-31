using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Data.DataContract;
using Data.DataContract.ArticeDC;
using ML.Common;
using Models.Artice;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class NewsController : AuthorisedController
    {
        public NewsController() : base(BePage.News) { }

        [View]
        public ActionResult Index(ArticeIndexModel model)
        {
            PopulateIndexModel(model);
            return View(model);
        }

        public ActionResult GetListArtice(ArticeIndexModel model)
        {
            PopulateIndexModel(model);

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_list", model)
            });
        }

        [View]
        public ActionResult EditArtice(Guid? id)
        {
            var model = new ArticeModel();

            if (id.HasValue)
            {
                var entity = ServiceHelper.Artice.ExecuteDispose(s => s.GetArtice(id.Value));

                if (entity == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                model = entity.Map<Artice, ArticeModel>();
                model.OldImagePath = model.ImagePath;
            }
            
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model),
                imagePath = Url.ImageIsExist(model.ImagePath) ? Url.ImageLink(model.ImagePath) : string.Empty
            });
        }

        public ActionResult SaveArtice(ArticeModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (!ModelState.IsValid)
            {
                return JsonObject(false, GetModelStateErrors());
            }

            #region save image

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

            var entity = model.Map<ArticeModel, Artice>();

            if (entity.IsNew)
            {
                entity.InitId();
                entity.CreatedDate = DateTime.UtcNow;
            }
            else
            {
                var oldData = ServiceHelper.Artice.ExecuteDispose(s => s.GetArtice(entity.Id));

                if (oldData == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                entity.CreatedDate = oldData.CreatedDate;
            }

            var request = new SaveRequest
            {
                Entity = entity
            };

            var res = ServiceHelper.Artice.ExecuteDispose(s => s.SaveArtice(request));

            if (res.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.SaveDataSuccess);
                return JsonObject(true, string.Empty);
            }

            var msg = res.Messages.FirstOrDefault();

            return JsonObject(false, string.IsNullOrWhiteSpace(msg) ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
        }

        [Delete]
        public ActionResult DeleteArtice(Guid id)
        {
            var response = ServiceHelper.Artice.ExecuteDispose(s => s.DeleteArtice(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.DeleteSuccessfull);
                return JsonObject(true, string.Empty);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        #region private

        private void PopulateIndexModel(ArticeIndexModel model)
        {
            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = "Name";
            }

            var filter = new FindRequest
            {
                TextSearch = model.TextSearch,
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) }),
                PageOption = new PageOption { PageSize = model.Pagination.PageSize, PageNumber = model.Pagination.CurrentPageIndex }
            };

            var response = ServiceHelper.Artice.ExecuteDispose(s => s.FindArtices(filter));
            model.Results = response.Results.MapList<ArticeModel>();
            model.Pagination.TotalRecords = response.TotalRecords;
        }

        #endregion
    }
}