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
using System.Data;
using ExcelUtil.Spreadsheet;
using StructureMap;
using Share.Helper.Cache;
using Share.Messages.BeScreens.CustomerRes;
using Share.Messages.BeScreens.ProductRes;

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
        #region export excel

        [Export]
        public ActionResult ExportProducts(ProductIndexModel model)
        {
            model.IsExport = true;
            PopulateIndexModel(model);

            var name = "Products";

            var ef = new ExcelFile();

            var ws = ef.Worksheets.Add(name);

            var ri = 0;
            var ci = 0;

            Action<ExcelCell> fontmatCaption = cell =>
            {
                cell.Style.Font.Weight = ExcelFont.BoldWeight;
                cell.Style.VerticalAlignment = VerticalAlignmentStyle.Top;
            };

            #region header

            ws.Cells[ri, ci].Value = "CategoryCode";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "ProductCode";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "UnitCode";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "Status";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "Cost";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "Vat";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "Name";
            fontmatCaption(ws.Cells[ri, ci]);

            ++ci;
            ws.Cells[ri, ci].Value = "Price";
            fontmatCaption(ws.Cells[ri, ci]);

            #endregion

            #region body
            var cache = ObjectFactory.GetInstance<ICacheHelper>();
            foreach (var item in model.Results)
            {
                ++ri;
                ci = 0;
                var category = cache.GetCategory(item.CategoryId);
                var unit = "";
                if (item.UnitId!=null)
                {
                     unit = cache.GetCategory(Guid.Parse(item.UnitId.ToString())).Code;
                }
                ws.Cells[ri, ci++].Value = category.Code;
                ws.Cells[ri, ci++].Value = item.ProductCode;
                ws.Cells[ri, ci++].Value = unit;
                ws.Cells[ri, ci++].Value = item.Status;
                ws.Cells[ri, ci++].Value = item.Cost;
                ws.Cells[ri, ci++].Value = item.Vat;
                ws.Cells[ri, ci++].Value = item.Name;
                ws.Cells[ri, ci++].Value = item.Price;
            }

            #endregion

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = name + ".xls",

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            var memoryStream = new MemoryStream();

            ef.SaveXls(memoryStream);

            memoryStream.Position = 0;

            return File(memoryStream, "application/ms-excel");
        }

        #endregion
        #region import excel
        [Import]
        public ActionResult ImportProducts()
        {
            var file = Request.Files["fileImport"];
            if (file == null || string.IsNullOrWhiteSpace(file.FileName))
            {
                return JsonObject(false, BackendMessage.DataInvalid);
            }

            var fileName = Path.GetFileName(file.FileName);

            var localFilePath = PhysicalDataFilePath(Guid.NewGuid() + Path.GetExtension(fileName), DataChildFolder.Files);

            if (!SiteUtils.IsExcelFile(fileName))
            {
                return JsonObject(false, BackendMessage.ExcelFileInvalid);
            }

            try
            {
                file.SaveAs(localFilePath);

                var table = LoadExcelFile(localFilePath);

                if (table == null || table.Rows.Count == 0)
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.ThereIsNoDataRows);
                }

                if (table.Columns.Count != 8)
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.ThereIsNotEnoughColumns);
                }

                //Check : must be have header row
                if (!CheckHaveHeaderRow(table))
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, BackendMessage.HeaderRowInvalid);
                }

                // Remove end empty row
                RemoveLastEmptyRow(table);

                //Check required field
                var listMessge = CheckInfoRequired(table);

                // Check email is valid format
                //listMessge.AddRange(CheckEmailValidFormat(table));

                if (listMessge.Any())
                {
                    DeleteFile(localFilePath);
                    return JsonObject(false, string.Join("<br/>", listMessge));
                }

                //Save
                var listProduct = new List<Product>();
                PopulateSaveImportData(listProduct, table);

                DeleteFile(localFilePath);

                var response = ServiceHelper.Product.ExecuteDispose(s => s.SaveProducts(listProduct));

                if (response.Success)
                {
                    return JsonObject(true, BackendMessage.ImportDataSuccessfull);
                }

                return JsonObject(false, GetMessageFromList(response.Messages));
            }
            catch (Exception e)
            {
                DeleteFile(localFilePath);
                return JsonObject(false, e.Message);
            }
        }


        public ActionResult GetImportForm()
        {
            return JsonObject(true, string.Empty, new
            {
                html = PartialViewToString("BaseView/Customer/Inc/_formImport")
            });
        }

        private DataTable LoadExcelFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToStr();
            var ef = new ExcelFile();
            if (extension.ToLower().EndsWith("xls"))
            {
                ef.LoadXls(fileName);
            }
            else if (extension.ToLower().EndsWith("csv"))
            {
                ef.LoadCsv(fileName, CsvType.CommaDelimited);
            }

            if (ef.Worksheets.Count == 0) return null;

            var ws = ef.Worksheets[0];

            if (ws.Rows.Count == 0) return null;

            var row = ws.Rows[0];

            var table = new DataTable();

            const int numCol = 7;

            for (var i = 0; i <= numCol; ++i)
            {
                var column = new DataColumn(row.Cells[i].Value.ToStr());
                table.Columns.Add(column);
            }

            for (var i = 1; i < ws.Rows.Count; ++i)
            {
                row = ws.Rows[i];
                var list = new List<object>();
                for (var colIndex = 0; colIndex <= numCol; ++colIndex)
                {
                    var cell = row.Cells[colIndex];
                    list.Add(cell.Value.ToStr());
                }

                table.Rows.Add(list.ToArray());
            }

            return table;
        }

        private bool CheckHaveHeaderRow(DataTable table)
        {
            if (!string.Equals(table.Columns[0].ColumnName, "CategoryCode", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[1].ColumnName, "ProductCode", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[2].ColumnName, "UnitCode", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[3].ColumnName, "Status", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[4].ColumnName, "Cost", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[5].ColumnName, "Vat", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[6].ColumnName, "Name", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (!string.Equals(table.Columns[7].ColumnName, "Price", StringComparison.CurrentCultureIgnoreCase)) return false;

            return true;
        }
        private void RemoveLastEmptyRow(DataTable table)
        {
            if (table.Rows.Count == 0) return;

            var listIndex = new List<int>();

            for (var i = table.Rows.Count - 1; i > 0; --i)
            {
                var lastRow = table.Rows[i];

                if (string.IsNullOrWhiteSpace(lastRow[0].ToStr()))
                {
                    listIndex.Add(i);
                }
                else
                {
                    break;
                }
            }

            listIndex.ForEach(i => table.Rows.RemoveAt(i));
        }
        private List<string> CheckInfoRequired(DataTable table)
        {
            var list = new List<string>();

            var index = 2;

            foreach (DataRow row in table.Rows)
            {
                var category = row[0].ToStr();
                var product = row[1].ToStr();
                var unit = row[2].ToStr();
                var status = row[3].ToStr();
                var productName = row[6].ToStr();
                var cost = row[4].ToStr();
                var vat = row[4].ToStr();

                if (string.IsNullOrWhiteSpace(category))
                {
                    list.Add(string.Format(ProductRes.ExcelCategoryCodeRequired, index));
                }

                if (string.IsNullOrWhiteSpace(product))
                {
                    list.Add(string.Format(ProductRes.ExcelProductCodeRequired, index));
                }

                if (string.IsNullOrWhiteSpace(unit))
                {
                    list.Add(string.Format(ProductRes.ExcelUOMRequired, index));
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    list.Add(string.Format(ProductRes.ExcelStatusRequired, index));
                }

                if (string.IsNullOrWhiteSpace(cost))
                {
                    list.Add(string.Format(ProductRes.ExcelCostRequired, index));
                }

                if (string.IsNullOrWhiteSpace(vat))
                {
                    list.Add(string.Format(ProductRes.ExcelVatRequired, index));
                }

                ++index;
            }

            return list;
        }
        private void PopulateSaveImportData(List<Product> listCustomer, DataTable table)
        {
             var cache = ObjectFactory.GetInstance<ICacheHelper>();

            foreach (DataRow row in table.Rows)
            {
                 var category = cache.GetCategoryByCode(row[0].ToStr()) ;
                 var unit = cache.GetCategoryByCode(row[2].ToStr());
                 var status = row[3].ToStr() == "10" ? ProductStatus.Active : ProductStatus.Inactive;
                var item = new Product
                {
                    CategoryId =category.Id,
                    ProductCode = row[1].ToStr(),
                    UnitId = unit.Id,
                    Status = status,
                    Cost =  row[4].ToStr().ToDecimal(),
                    Vat = row[5].ToStr().ToDecimal(),
                    Name = row[6].ToStr(),
                    Price = row[7].ToStr().ToDecimal(),
                    CreatedDate = DateTime.Now,
                    UpdateDate=DateTime.Now
                };

                item.InitId();

                listCustomer.Add(item);
            }
        }
        #endregion
    }
}