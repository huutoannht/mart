using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.CategoryDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "Category")]
    public interface ICategoryService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindCategories(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<Category> GetAllCategory(bool includeProduct = false);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveCategory(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteCategory(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveImage(CategoryImage categoryImage);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveListImage(List<CategoryImage> listImage);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteImage(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<CategoryImage> GetImages(Guid categoryId);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        CategoryImage GetImage(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateImageInfo(CategoryImageUpdateInfo info);
    }
}
