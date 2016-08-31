using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.CategoryDC;
using Db.Interfaces;
using Service.Contract;
using Share.Helper.Cache;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ICacheHelper _cacheHelper;

        public CategoryService(ICategoryRepository categoryRepository, ICacheHelper cacheHelper)
        {
            _repository = categoryRepository;
            _cacheHelper = cacheHelper;
        }

        public FindResponse FindCategories(FindRequest request)
        {
            return Execute(_repository, r => r.FindCategories(request));
        }

        public List<Category> GetAllCategory(bool includeProduct = false)
        {
            return Execute(_repository, r => r.GetAllCategory(includeProduct));
        }

        public BaseResponse SaveCategory(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                #region check

                var entity = request.Entity;

                if (!string.IsNullOrWhiteSpace(entity.Code))
                {
                    var isNew = _cacheHelper.GetCategory(entity.Id) == null;

                    if (isNew)
                    {
                        if (r.CodeExists(entity.Code, entity.CategoryType))
                        {
                            res.Success = false;
                            res.Messages.Add("CodeExisted"); //res key
                            return res;
                        }
                    }
                    else if (r.CodeExists(entity.Code, entity.CategoryType, entity.Id))
                    {
                        res.Success = false;
                        res.Messages.Add("CodeExisted");//res key
                        return res;
                    }
                }

                #endregion

                res = r.SaveCategory(request);

                _cacheHelper.ClearGetCategories();

                return res;
            });
        }

        public BaseResponse DeleteCategory(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                if (r.CategoryIsUsed(id))
                {
                    res.Messages.Add("CategoryIsUsedCanNotDelete");//res key
                    res.Success = false;
                    return res;
                }

                var cate = _cacheHelper.GetCategories().FirstOrDefault(i => i.Id == id);

                res = r.DeleteCategory(id);

                if (res.Success && cate != null)
                {
                    DeleteFile(dataFolderPath, cate.ImagePath);

                    cate.Images.ForEach(i => DeleteFile(dataFolderPath, i.ImagePath));
                }

                _cacheHelper.ClearGetCategories();

                return res;
            });
        }

        public BaseResponse SaveImage(CategoryImage categoryImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveImage(categoryImage);

                _cacheHelper.ClearGetCategories();

                return res;
            });
        }

        public BaseResponse SaveListImage(List<CategoryImage> listImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveListImage(listImage);

                _cacheHelper.ClearGetCategories();

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

                return res;
            });
        }

        public List<CategoryImage> GetImages(Guid categoryId)
        {
            return Execute(_repository, r => r.GetImages(categoryId));
        }

        public CategoryImage GetImage(Guid id)
        {
            return Execute(_repository, r => r.GetImage(id));
        }

        public BaseResponse UpdateImageInfo(CategoryImageUpdateInfo info)
        {
            return Execute(_repository, r =>
            {
                var res = r.UpdateImageInfo(info);

                _cacheHelper.ClearGetCategories();

                return res;
            });
        }
    }
}
