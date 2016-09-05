using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.ProductDC;
using Db.Interfaces;
using Service.Contract;
using Share.Helper;
using Share.Helper.Cache;
using Share.Messages.ServiceMessage;
using Share.Messages.BeScreens.ProductRes;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICacheHelper _cacheHelper;

        public ProductService(IProductRepository repository, ICacheHelper cacheHelper)
        {
            _repository = repository;
            _cacheHelper = cacheHelper;
        }

        public List<Product> GetAll()
        {
            return Execute(_repository, r => r.GetAll());
        }

        public bool ResetImageRepresent(Guid productId)
        {
            return Execute(_repository, r =>
            {
                var res = r.ResetImageRepresent(productId);

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }

        public FindResponse FindProducts(FindRequest request)
        {
            return Execute(_repository, r => r.FindProducts(request));
        }

        public Product GetProduct(Guid id)
        {
            return Execute(_repository, r => r.GetProduct(id));
        }

        public Product GetProductByProductCode(string productCode)
        {
            return Execute(_repository, r => r.GetProduct(productCode));
        }

        public BaseResponse UpdateProductInfo(ProductUpdateInfo info)
        {
            return Execute(_repository, r =>
            {
                var res = r.UpdateProductInfo(info);

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }

        public BaseResponse SaveProduct(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                var entity = request.Entity;

                #region check

                var isNew = !r.ProductIsExisted(entity.Id);

                if (isNew)
                {
                    if (r.ProductCodeExists(entity.ProductCode))
                    {
                        res.Success = false;
                        res.Messages.Add("CodeExisted"); //res key
                        return res;
                    }
                }
                else if (r.ProductCodeExists(entity.ProductCode, entity.Id))
                {
                    res.Success = false;
                    res.Messages.Add("CodeExisted");//res key
                    return res;
                }

                #endregion

                #region check category has no child

                if (entity.CategoryId == Guid.Empty)
                {
                    res.Success = false;
                    res.Messages.Add("DataIsInvalid");//res key
                    return res;
                }

                var allCates = _cacheHelper.GetCategories();
                var cate = allCates.FirstOrDefault(i => i.Id == entity.CategoryId);

                if (cate == null)
                {
                    res.Success = false;
                    res.Messages.Add("DataIsInvalid");//res key
                    return res;
                }

                if (allCates.Any(i => i.ParentId == cate.Id))//cate has childs
                {
                    res.Success = false;
                    res.Messages.Add("CannotAddNewIntoThisGroup");//res key
                    return res;
                }

                #endregion

                res = r.SaveProduct(request);

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }

        public BaseResponse DeleteProduct(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var product = r.GetProduct(id);

                var res = new BaseResponse();

                if (product == null) return res;

                res = r.DeleteProduct(id);

                if (res.Success)
                {
                    product.ProductImages.ForEach(image => DeleteFile(dataFolderPath, image.ImagePath));
                    _cacheHelper.ClearGetProducts();
                }

                return res;
            });
        }

        public BaseResponse SaveImage(ProductImage productImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveImage(productImage);
                _cacheHelper.ClearGetProducts();
                return res;
            });
        }

        public BaseResponse SaveListImage(List<ProductImage> listImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveListImage(listImage);

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }

        public BaseResponse DeleteImages(List<Guid> ids, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var images = r.GetImages(ids);

                var res = r.DeleteImages(ids);

                if (res.Success)
                {
                    images.ForEach(image => DeleteFile(dataFolderPath, image.ImagePath));
                    _cacheHelper.ClearGetProducts();
                }

                return res;
            });
        }

        public BaseResponse DeleteImage(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {

                var res = new BaseResponse();

                var image = r.GetImage(id);

                if (image == null)
                {
                    res.Success = false;
                    res.Messages.Add("DataNotFound"); //res key
                    return res;
                }

                res = r.DeleteImage(id);

                if (res.Success)
                {
                    DeleteFile(dataFolderPath, image.ImagePath);
                }

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }

        public List<ProductImage> GetImages(Guid productId)
        {
            return Execute(_repository, r => r.GetImages(productId));
        }

        public BaseResponse UpdateImageInfo(ProductImageUpdateInfo info)
        {
            return Execute(_repository, r =>
            {
                var res = r.UpdateImageInfo(info);

                _cacheHelper.ClearGetProducts();

                return res;
            });
        }


        public BaseResponse SaveProducts(List<Product> products)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                var rowIndex = 2;
                products.ForEach(product =>
                {
                    if (r.ProductCodeExists(product.ProductCode))
                    {
                        res.Messages.Add(
                            string.Format(ProductRes.ExcelProductCodeExisted,
                            rowIndex,
                            product.ProductCode));
                    }
                    ++rowIndex;
                });

                if (res.Messages.Any())
                {
                    res.Success = false;
                    return res;
                }

                res = r.SaveProducts(products);

                return res;
            });
        }
    }
}
