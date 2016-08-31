using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Data.DataContract;
using Data.DataContract.CategoryDC;
using ML.Common;
using Models.Category;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class CategoryBaseController : AuthorisedController
    {
        protected CategoryType CategoryType { get; set; }

        protected string AddNewTitle { get; set; }
        protected string EditTitle { get; set; }

        public CategoryBaseController(BePage bePage) : base(bePage) { }

        public ActionResult GetList(CategoryIndexModel model)
        {
            PopulateIndexModel(model);

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Nhom/_list", model)
            });
        }

        [View]
        public ActionResult AddNew(Guid? parentId)
        {
            ViewBag.AddNewTitle = AddNewTitle;

            var model = new CategoryModel
            {
                ParentId = parentId,
                CategoryType = CategoryType
            };

            if (parentId.HasValue)
            {
                var parent = SiteUtils.BuildCategoryListFromTree().FirstOrDefault(i => i.Id == parentId.Value);
                if (parent != null && parent.Childs.Any())
                {
                    model.SortOrder = parent.Childs.Max(i => i.SortOrder) + 1;
                }
            }
            else
            {
                var cates = CacheHelper.GetCategoryTreeList(CategoryType);
                if (cates.Any())
                {
                    model.SortOrder = cates.Max(i => i.SortOrder) + 1;
                }
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Nhom/_edit", model)
            });
        }

        [View]
        public ActionResult Edit(Guid id)
        {
            ViewBag.EditTitle = EditTitle;

            var entity = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == id);

            if (entity == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            var model = entity.Map<Category, CategoryModel>();
            model.OldImagePath = model.ImagePath;

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Nhom/_edit", model),
                imagePath = Url.ImageIsExist(model.ImagePath) ? Url.ImageLink(model.ImagePath) : string.Empty
            });
        }

        public ActionResult Save(CategoryModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (model.CategoryType != CategoryType.Product && model.CategoryType != CategoryType.UOM)
            {
                ModelState.Remove("Code");
            }

            if (!ModelState.IsValid)
            {
                return JsonObject(false, GetModelStateErrors());
            }

            var entity = model.Map<CategoryModel, Category>();

            if (entity.IsNew)
            {
                entity.InitId();
            }

            var request = new SaveRequest
            {
                Entity = entity
            };

            var res = ServiceHelper.Category.ExecuteDispose(s => s.SaveCategory(request));

            if (res.Success)
            {
                var indexModel = new CategoryIndexModel();
                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.SaveDataSuccess, new
                {
                    html = PartialViewToString("BaseView/Nhom/_list", indexModel)
                });
            }

            var msg = res.Messages.FirstOrDefault();

            return JsonObject(false, string.IsNullOrWhiteSpace(msg) ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
        }

        [Update]
        public ActionResult SaveFile(CategoryModel model)
        {
            if (Request.Files["menuFileImage"] != null && !string.IsNullOrWhiteSpace(Request.Files["menuFileImage"].FileName))
            {
                var file = Request.Files["menuFileImage"];

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

            #region save list image

            var listImagePath = new List<string>();

            for (var i = 0; i < Request.Files.AllKeys.Length; ++i)
            {
                var file = Request.Files[Request.Files.AllKeys[i]];
                if (file != null &&
                    !string.IsNullOrWhiteSpace(file.FileName) && Request.Files.AllKeys[i].StartsWith("fileImage"))
                {
                    var extension = Path.GetExtension(file.FileName).ToStr();

                    if (!SiteUtils.IsImageFile(file.FileName))
                    {
                        return JsonObject(false, BackendMessage.FileTypeIsInvalid);
                    }

                    if (!SiteUtils.ImageSizeIsValid(file.ContentLength))
                    {
                        return JsonObject(false, BackendMessage.FileMaxSize5MB);
                    }

                    var imagePath = Guid.NewGuid() + extension;
                    listImagePath.Add(imagePath);

                    var filePath = PhysicalDataFilePath(imagePath);
                    file.SaveAs(filePath);
                }
            }

            #endregion

            return JsonObject(true, string.Empty, new
            {
                imagePath = model.ImagePath,
                imageFileNames = string.Join(";", listImagePath)
            });
        }

        [Delete]
        public ActionResult Delete(Guid id)
        {
            var entity = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == id);

            if (entity == null)
            {
                var indexModel = new CategoryIndexModel();
                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    html = PartialViewToString("BaseView/Nhom/_list", indexModel)
                });
            }

            var res = ServiceHelper.Category.ExecuteDispose(s => s.DeleteCategory(id, PhysicalDataImagesFolderPath));

            if (res.Success)
            {
                var indexModel = new CategoryIndexModel();
                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    html = PartialViewToString("BaseView/Nhom/_list", indexModel)
                });
            }

            var msg = res.Messages.FirstOrDefault();

            return JsonObject(false, string.IsNullOrWhiteSpace(msg) ? BackendMessage.ErrorOccure : msg.GetServiceMessageRes());
        }

        #region images

        public ActionResult GetImageList(Guid categoryId)
        {
            var cate = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == categoryId);

            if (cate == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                imageList = PartialViewToString("BaseView/Nhom/ImageList/_imageList", cate.Map<Category, CategoryModel>())
            });
        }

        public ActionResult GetAddNewImageHtml(Guid categoryId)
        {
            var cate = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == categoryId);

            if (cate == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Nhom/ImageList/_addNewImage", cate.Map<Category, CategoryModel>())
            });
        }

        public ActionResult UpdateImageVisible(Guid id, bool visible)
        {
            var updateInfo = new CategoryImageUpdateInfo
            {
                Id = id,
                Visible = visible
            };

            var response = ServiceHelper.Category.ExecuteDispose(s => s.UpdateImageInfo(updateInfo));

            if (response.Success)
            {
                return JsonObject(true, BackendMessage.SaveDataSuccess);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        public ActionResult DeleteImage(Guid id, Guid categoryId)
        {
            var response = ServiceHelper.Category.ExecuteDispose(s => s.DeleteImage(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                var cate = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == categoryId);

                if (cate == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                var indexModel = new CategoryIndexModel();

                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    categoryList = PartialViewToString("BaseView/Nhom/_list", indexModel),
                    imageList = PartialViewToString("BaseView/Nhom/ImageList/_imageList", cate.Map<Category, CategoryModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        public ActionResult SaveImage(Guid categoryId, bool visible)
        {
            var imagePath = string.Empty;

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

                imagePath = Guid.NewGuid() + extension;

                var filePath = PhysicalDataFilePath(imagePath);
                file.SaveAs(filePath);
            }

            #endregion

            var image = new CategoryImage
            {
                ImagePath = imagePath,
                CategoryId = categoryId,
                CreatedDate = DateTime.UtcNow,
                Visible = visible
            };

            image.InitId();

            var response = ServiceHelper.Category.ExecuteDispose(s => s.SaveImage(image));

            if (response.Success)
            {
                var cate = CacheHelper.GetCategories().FirstOrDefault(i => i.Id == categoryId);

                if (cate == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                var indexModel = new CategoryIndexModel();

                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.SaveDataSuccess, new
                {
                    categoryList = PartialViewToString("BaseView/Nhom/_list", indexModel),
                    imageList = PartialViewToString("BaseView/Nhom/ImageList/_imageList", cate.Map<Category, CategoryModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        #endregion

        #region protected

        protected void PopulateIndexModel(CategoryIndexModel model)
        {
            model.CategoryType = CategoryType;

            model.InitSortInfo();

            if (string.IsNullOrWhiteSpace(model.SortBy))
            {
                model.SortBy = CategoryType == CategoryType.Product ? "Code" : "Name";
            }

            var filter = new FindRequest
            {
                TextSearch = model.TextSearch,
                CategoryType = CategoryType,
                SortOption = new SortOption(new[] { new SortItem(model.SortBy, model.SortDirection.Value) })
            };

            var response = ServiceHelper.Category.ExecuteDispose(s => s.FindCategories(filter));

            var listTree = SiteUtils.BuildCategoryTreeFromList(response.Results);

            model.Results = listTree.MapList<CategoryModel>();

            model.Pagination.TotalRecords = response.TotalRecords;
        }

        #endregion
    }
}