using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.CategoryDC;

namespace Db.Interfaces
{
    public interface ICategoryRepository
    {
        FindResponse FindCategories(FindRequest request);

        bool CategoryIsUsed(Guid categoryId);

        List<Category> GetAllCategory(bool includeProduct = false);

        BaseResponse SaveCategory(SaveRequest request);

        BaseResponse DeleteCategory(Guid id);

        BaseResponse SaveImage(CategoryImage categoryImage);

        BaseResponse SaveListImage(List<CategoryImage> listImage);

        BaseResponse DeleteImage(Guid id);

        List<CategoryImage> GetImages(Guid categoryId);

        CategoryImage GetImage(Guid id);

        BaseResponse UpdateImageInfo(CategoryImageUpdateInfo info);

        bool CodeExists(string code, CategoryType categoryType);

        bool CodeExists(string code, CategoryType categoryType, Guid id);
    }
}
