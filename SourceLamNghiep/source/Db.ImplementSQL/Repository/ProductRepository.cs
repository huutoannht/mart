using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ML.Common;
using Data.DataContract;
using Data.DataContract.ProductDC;
using Db.Interfaces;

namespace Db.ImplementSQL.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        #region product

        public bool ProductIsExisted(Guid id)
        {
            using (var db = DbContext)
            {
                return db.Products.Any(i => i.Id == id);
            }
        }

        public Product GetProduct(Guid id)
        {
            using (var db = DbContext)
            {
                var query = db.Products
                    .Include("ProductImages")
                    .AsQueryable();

                return query.FirstOrDefault(i => i.Id == id).Map<Entity.Product, Product>();
            }
        }

        public Product GetProduct(string productCode)
        {
            using (var db = DbContext)
            {
                var query = db.Products
                    .Include("ProductImages")
                    .AsQueryable();

                return query.FirstOrDefault(i => i.ProductCode == productCode).Map<Entity.Product, Product>();
            }
        }

        public BaseResponse SaveProduct(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<Product, Entity.Product>();

                if (!db.Products.Any(e => e.Id == entityDb.Id))
                {
                    db.Products.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse SaveProducts(List<Product> products)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (products == null || !products.Any()) return response;

                products.ForEach(customer => db.Products.Add(customer.Map<Product, Entity.Product>()));
                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }
        public BaseResponse DeleteProduct(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Products.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;

                    if (response.Success)
                    {
                        db.Database.ExecuteSqlCommand("DELETE ProductImages WHERE ProductId = {0}", id);
                        db.Database.ExecuteSqlCommand("DELETE ProductVideos WHERE ProductId = {0}", id);
                    }
                }
            }

            return response;
        }

        public BaseResponse UpdateProductInfo(ProductUpdateInfo info)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = new Entity.Product { Id = info.Id };
                db.Products.Attach(entityDb);

                var changed = false;

                if (changed)
                {
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public FindResponse FindProducts(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Products
                    .Include("ProductImages")
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.Trim().ToLower();
                    query = query.Where(q =>
                        q.Name.ToLower().Contains(request.TextSearch)
                        || q.ProductCode.ToLower().Contains(request.TextSearch)
                        || q.Category.Name.ToLower().Contains(request.TextSearch)
                        || q.Unit.Name.ToLower().Contains(request.TextSearch)
                    );
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<Product>();
            }

            return response;
        }

        public bool ProductCodeExists(string code)
        {
            code = code.ToStr().ToLower().Trim();
            using (var db = DbContext)
            {
                return db.Products.Any(i => i.ProductCode.ToLower().Trim() == code);
            }
        }

        public bool ProductCodeExists(string code, Guid id)
        {
            code = code.ToStr().ToLower().Trim();
            using (var db = DbContext)
            {
                return db.Products.Any(i => i.ProductCode.ToLower().Trim() == code && i.Id != id);
            }
        }

        public List<Product> GetAll()
        {
            using (var db = DbContext)
            {
                return db.Products.OrderBy(i => i.Name).ToList().MapList<Product>();
            }
        }

        #endregion

        #region image

        public BaseResponse SaveImage(ProductImage productImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = productImage.Map<ProductImage, Entity.ProductImage>();

                if (!db.ProductImages.Any(e => e.Id == entityDb.Id))
                {
                    db.ProductImages.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse SaveListImage(List<ProductImage> listImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (listImage.Any())
                {
                    listImage.ForEach(productImage =>
                    {
                        var entityDb = productImage.Map<ProductImage, Entity.ProductImage>();
                        db.ProductImages.Add(entityDb);

                    });

                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public BaseResponse DeleteImage(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.ProductImages.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public BaseResponse DeleteImages(List<Guid> ids)
        {
            var response = new BaseResponse();

            if (ids == null || !ids.Any()) return response;

            using (var db = DbContext)
            {
                ids.ForEach(id =>
                {
                    var entity = db.ProductImages.FirstOrDefault(e => e.Id == id);

                    if (entity != null)
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                    }
                });

                response.Success = db.SaveChanges() > 0;
                return response;
            }
        }

        public List<ProductImage> GetImages(Guid productId)
        {
            using (var db = DbContext)
            {
                return
                    db.ProductImages.Where(i => i.ProductId == productId)
                        .ToList()
                        .MapList<ProductImage>();
            }
        }

        public List<ProductImage> GetImages(List<Guid> ids)
        {
            using (var db = DbContext)
            {
                if (ids == null || !ids.Any()) return new List<ProductImage>();

                return db.ProductImages.Where(i => ids.Contains(i.Id)).ToList().MapList<ProductImage>();
            }
        }

        public ProductImage GetImage(Guid id)
        {
            using (var db = DbContext)
            {
                return db.ProductImages.FirstOrDefault(i => i.Id == id).Map<Entity.ProductImage, ProductImage>();
            }
        }

        public BaseResponse UpdateImageInfo(ProductImageUpdateInfo info)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (!db.ProductImages.Any(i => i.Id == info.Id))
                {
                    return response;
                }

                var entityDb = new Entity.ProductImage { Id = info.Id };
                db.ProductImages.Attach(entityDb);

                var changed = false;

                if (info.Visible.HasValue)
                {
                    entityDb.Visible = info.Visible.Value;
                    db.Entry(entityDb).Property(o => o.Visible).IsModified = true;
                    changed = true;
                }

                if (info.Represent.HasValue)
                {
                    entityDb.Represent = info.Represent.Value;
                    db.Entry(entityDb).Property(o => o.Represent).IsModified = true;
                    changed = true;
                }

                if (changed)
                {
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public bool ResetImageRepresent(Guid productId)
        {
            using (var db = DbContext)
            {
                db.Database.ExecuteSqlCommand("Update ProductImages set Represent = 0 where ProductId = {0}", productId);
                return true;
            }
        }

        #endregion
    }
}
