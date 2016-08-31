using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ML.Common;
using Data.DataContract;
using Data.DataContract.ProductDC;
using Models.Product;
using Share.Helper;
using Share.Helper.Extension;
using Share.Messages;
using Site.Backend.Library.Attribute;
using Site.Backend.Library.UI;

namespace Site.Backend.Controllers
{
    public class SanPhamController : AuthorisedController
    {
        public SanPhamController() : base(BePage.SanPham) { }

        [View]
        public ActionResult Index(ProductIndexModel model)
        {
            PopulateIndexModel(model);

            return View(model);
        }

        public ActionResult GetListProduct(ProductIndexModel model)
        {
            PopulateIndexModel(model);

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_list", model)
            });
        }

        [View]
        public ActionResult EditProduct(Guid? id, Guid? categoryId)
        {
            ProductModel model;

            if (id.HasValue)
            {
                var entity = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(id.Value));
                if (entity == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                model = entity.Map<Product, ProductModel>();
            }
            else
            {
                model = new ProductModel
                {
                    Status = ProductStatus.Active
                };

                if (categoryId.HasValue)
                {
                    model.CategoryId = categoryId.Value;
                }
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("_edit", model)
            });
        }

        public ActionResult SaveProduct(ProductModel model)
        {
            if (model.IsNew && !CanAdd)
            {
                return GetAddDeniedResult();
            }

            if (!model.IsNew && !CanUpdate)
            {
                return GetUpdateDeniedResult();
            }

            if (!ModelState.IsValid && GetModelStateErrorList().Any())
            {
                return JsonObject(false, GetModelStateErrors());
            }

            #region save list image

            var listImagePath = new List<string>();

            for (var i = 0; i < Request.Files.Count; ++i)
            {
                var file = Request.Files[i];
                if (file != null &&
                    !string.IsNullOrWhiteSpace(file.FileName))
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

            var entity = model.Map<ProductModel, Product>();

            if (entity.IsNew)
            {
                entity.InitId();
                entity.CreatedDate = DateTime.UtcNow;
                entity.UpdateDate = DateTime.UtcNow;
            }
            else
            {
                var oldData = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(entity.Id));
                if (oldData == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                entity.CreatedDate = oldData.CreatedDate;
                entity.UpdateDate = DateTime.UtcNow;
            }

            var response = ServiceHelper.Product.ExecuteDispose(s => s.SaveProduct(new SaveRequest
            {
                Entity = entity
            }));

            if (response.Success)
            {
                if (listImagePath.Any())
                {
                    var listImages = new List<ProductImage>();
                    listImagePath.ForEach(imagePath =>
                    {
                        var image = new ProductImage
                        {
                            ImagePath = imagePath,
                            ProductId = entity.Id,
                            CreatedDate = DateTime.UtcNow,
                            Visible = true
                        };

                        image.InitId();

                        listImages.Add(image);
                    });


                    ServiceHelper.Product.ExecuteDispose(s => s.SaveListImage(listImages));
                }

                var indexModel = new ProductIndexModel
                {
                    TextSearch = model.TextSearch,
                };

                PopulateIndexModel(indexModel);

                return JsonObject(true, BackendMessage.SaveDataSuccess, new
                {
                    html = PartialViewToString("_list", indexModel)
                });
            }

            if (listImagePath.Any())
            {
                listImagePath.ForEach(DeleteImageFile);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Delete]
        public ActionResult DeleteProduct(Guid id, ProductIndexModel indexModel)
        {
            var response = ServiceHelper.Product.ExecuteDispose(s => s.DeleteProduct(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                indexModel.Pagination.CurrentPageIndex = 1;
                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    html = PartialViewToString("_list", indexModel)
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        public ActionResult GetDropDownDistrictsWards(ProductModel model)
        {
            return JsonObject(true, string.Empty, new
            {
                dropDownDistrict = PartialViewToString("Inc/_dropDownDistrict1", model),
                dropDownWard = PartialViewToString("Inc/_dropDownWard1", model),
                dropDownProject = PartialViewToString("Inc/_dropDownProjects", model),
            });
        }

        public ActionResult GetDropDownWards(ProductModel model)
        {
            return JsonObject(true, string.Empty, new
            {
                dropDownWard = PartialViewToString("Inc/_dropDownWard1", model),
                dropDownProject = PartialViewToString("Inc/_dropDownProjects", model),
            });
        }

        #region images

        public ActionResult GetImageList(Guid productId)
        {
            var product = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(productId));

            if (product == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                imageList = PartialViewToString("ImageList/_productImageList", product.Map<Product, ProductModel>())
            });
        }

        public ActionResult GetAddNewImageHtml(Guid productId)
        {
            var product = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(productId));

            if (product == null)
            {
                return JsonObject(false, BackendMessage.CannotLoadData);
            }

            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("ImageList/_addNewImage", product.Map<Product, ProductModel>())
            });
        }

        [Update]
        public ActionResult SaveImage(Guid productId, bool? visible, string textSearch)
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

            var productImage = new ProductImage
            {
                ImagePath = imagePath,
                ProductId = productId,
                CreatedDate = DateTime.UtcNow,
                Visible = (visible==null)?false:true
            };

            productImage.InitId();

            var response = ServiceHelper.Product.ExecuteDispose(s => s.SaveImage(productImage));

            if (response.Success)
            {
                var product = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(productId));
                if (product == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                var indexModel = new ProductIndexModel
                {
                    TextSearch = textSearch
                };

                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.SaveDataSuccess, new
                {
                    productList = PartialViewToString("_list", indexModel),
                    imageList = PartialViewToString("ImageList/_productImageList", product.Map<Product, ProductModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Update]
        public ActionResult DeleteImage(Guid id, Guid productId, ProductIndexModel indexModel)
        {
            var response = ServiceHelper.Product.ExecuteDispose(s => s.DeleteImage(id, PhysicalDataImagesFolderPath));

            if (response.Success)
            {
                var product = ServiceHelper.Product.ExecuteDispose(s => s.GetProduct(productId));
                if (product == null)
                {
                    return JsonObject(false, BackendMessage.CannotLoadData);
                }

                PopulateIndexModel(indexModel);
                return JsonObject(true, BackendMessage.DeleteSuccessfull, new
                {
                    productList = PartialViewToString("_list", indexModel),
                    imageList = PartialViewToString("ImageList/_productImageList", product.Map<Product, ProductModel>())
                });
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        [Update]
        public ActionResult UpdateImageVisible(Guid id, bool visible)
        {
            var updateInfo = new ProductImageUpdateInfo
            {
                Id = id,
                Visible = visible
            };

            var response = ServiceHelper.Product.ExecuteDispose(s => s.UpdateImageInfo(updateInfo));

            if (response.Success)
            {
                return JsonObject(true, BackendMessage.SaveDataSuccess);
            }

            return JsonObject(false, response.Messages.FirstOrDefault().GetServiceMessageRes());
        }

        #endregion

        #region private

        private void PopulateIndexModel(ProductIndexModel model)
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

            var response = ServiceHelper.Product.ExecuteDispose(s => s.FindProducts(filter));
            model.Results = response.Results.MapList<ProductModel>();
            model.Pagination.TotalRecords = response.TotalRecords;
        }

        #endregion
    }
}