using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.ProductDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "Product")]
    public interface IProductService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<Product> GetAll();

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        bool ResetImageRepresent(Guid productId);
       
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindProducts(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        Product GetProduct(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        Product GetProductByProductCode(string productCode);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateProductInfo(ProductUpdateInfo info);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveProduct(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteProduct(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveImage(ProductImage productImage);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveListImage(List<ProductImage> listImage);

        [OperationContract]
        [FaultContract(typeof (ErrorManager))]
        BaseResponse DeleteImages(List<Guid> ids, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteImage(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<ProductImage> GetImages(Guid productId);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateImageInfo(ProductImageUpdateInfo info);
    }
}
