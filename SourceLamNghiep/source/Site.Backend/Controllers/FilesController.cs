using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataContract;
using Data.DataContract.FileDC;
using ML.Common;
using Models.File;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Share.Messages.BeScreens.Files;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class FilesController : AuthorisedController
    {
        public FilesController() : base(BePage.Files) { }

        [View]
        public ActionResult Index(FileIndexModel model)
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

            var response = ServiceHelper.File.ExecuteDispose(s => s.FindFiles(filter));
            model.Results = response.Results.MapList<FileModel>();
            model.Pagination.TotalRecords = response.TotalRecords;

            return View(model);
        }

        [View]
        public ActionResult Add()
        {
            var model = new FileModel();

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model)
            });
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            var file = ServiceHelper.File.ExecuteDispose(s => s.GetFile(id));
            if (file == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var model = file.Map<Data.DataContract.FileDC.File, FileModel>();
            model.OldPhysicName = model.PhysicName;

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model)
            });
        }

        public ActionResult Save(FileModel model)
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

            #region file hanlder

            if (Request.Files.Count == 0 || string.IsNullOrWhiteSpace(Request.Files[0].FileName))
            {
                return JsonObject(false, Files.FileIsRequired);
            }

            if (!model.IsNew)
            {
                DeleteFile(model.OldPhysicName);
            }

            var file = Request.Files[0];

            var extension = Path.GetExtension(file.FileName).ToStr();

            if (SiteUtils.IsDangerousFile(file.FileName))
            {
                return JsonObject(false, BackendMessage.FileTypeIsInvalid);
            }

            if (!string.IsNullOrWhiteSpace(model.OldPhysicName))
            {
                DeleteFile(model.OldPhysicName);
            }

            model.PhysicName = Guid.NewGuid() + extension;

            var filePath = PhysicalDataFilePath(model.PhysicName, DataChildFolder.Files);
            file.SaveAs(filePath);

            #endregion

            var entity = model.Map<FileModel, Data.DataContract.FileDC.File>();
            if (entity.IsNew)
            {
                entity.InitId();
            }

            entity.Name = entity.Name.ToStr().Trim();
            entity.CreatedBy = CurrentUserId;
            entity.CreatedDate = DateTime.UtcNow;

            var request = new SaveRequest
            {
                Entity = entity
            };

            var res = ServiceHelper.File.ExecuteDispose(s => s.SaveFile(request));

            if (res.Success)
            {
                SendNotification(NotifyType.Success, Files.UploadFileSuccess);
                return JsonObject(true, string.Empty);
            }

            return JsonObject(false, res.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Delete]
        public ActionResult Delete(Guid id)
        {
            var file = ServiceHelper.File.ExecuteDispose(s => s.GetFile(id));
            if (file == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var response = ServiceHelper.File.ExecuteDispose(s => s.DeleteFile(id, PhysicalDataFilesFolderPath));

            if (response.Success)
            {
                SendNotification(NotifyType.Success, BackendMessage.DeleteSuccessfull);
                return JsonObject(true, string.Empty);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [View]
        public ActionResult Download(Guid id)
        {
            var file = ServiceHelper.File.ExecuteDispose(s => s.GetFile(id));
            if (file == null)
            {
                return Content(string.Empty);
            }

            var fileVirtualPath = PhysicalDataFilePath(file.PhysicName, DataChildFolder.Files);

            if (!System.IO.File.Exists(fileVirtualPath))
            {
                return Content(string.Empty);
            }

            var encoding = System.Text.Encoding.UTF8;
            Response.Charset = encoding.WebName;
            Response.HeaderEncoding = encoding;

            return File(fileVirtualPath, MimeMapping.GetMimeMapping(file.PhysicName), file.Name);
        }

        public ActionResult VerifyNameIsExisted(string name, Guid id)
        {
            var existed = id == Guid.Empty ? ServiceHelper.File.ExecuteDispose(s => s.CheckNameExisted(name)) : ServiceHelper.File.ExecuteDispose(s => s.CheckNameExisted2(name, id));
            return JsonObject(true, string.Empty, new
            {
                existed
            });
        }
    }
}