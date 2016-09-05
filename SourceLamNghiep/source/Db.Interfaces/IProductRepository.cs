using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.ProductDC;

namespace Db.Interfaces
{
    public interface IProductRepository
    {
        bool ProductIsExisted(Guid id);

        bool ResetImageRepresent(Guid productId);

        Product GetProduct(Guid id);

        Product GetProduct(string productCode);

        BaseResponse SaveProduct(SaveRequest request);

        BaseResponse SaveProducts(List<Product> products);

        BaseResponse DeleteProduct(Guid id);

        BaseResponse SaveImage(ProductImage productImage);

        BaseResponse SaveListImage(List<ProductImage> listImage);

        BaseResponse DeleteImages(List<Guid> ids);

        BaseResponse DeleteImage(Guid id);

        List<ProductImage> GetImages(Guid productId);

        List<ProductImage> GetImages(List<Guid> ids);

        ProductImage GetImage(Guid id);

        BaseResponse UpdateImageInfo(ProductImageUpdateInfo info);

        BaseResponse UpdateProductInfo(ProductUpdateInfo info);

        FindResponse FindProducts(FindRequest request);

        bool ProductCodeExists(string code);

        bool ProductCodeExists(string code, Guid id);

        List<Product> GetAll();
    }
}
