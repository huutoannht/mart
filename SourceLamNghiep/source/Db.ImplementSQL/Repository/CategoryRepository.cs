using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DataContract;
using Data.DataContract.CategoryDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public FindResponse FindCategories(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Categories
                    .AsQueryable();

                if (request.CategoryType.HasValue)
                {
                    query = query.Where(i => i.CategoryType == request.CategoryType);
                }

                if (!string.IsNullOrWhiteSpace(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.Trim().ToLower();
                    query = query.Where(i =>
                        i.Code.ToLower().Contains(request.TextSearch)
                        || i.Name.ToLower().Contains(request.TextSearch)
                        );
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<Category>();
            }

            return response;
        }

        public bool CategoryIsUsed(Guid categoryId)
        {
            using (var db = DbContext)
            {
                return db.Products.Any(i => i.CategoryId == categoryId);
            }
        }

        public List<Category> GetAllCategory(bool includeProduct = false)
        {
            using (var db = DbContext)
            {
                var query = db.Categories
                    .Include("Images")
                    .AsQueryable();

                return query.OrderBy(i => i.Name).ToList().MapList<Category>();
            }
        }

        public BaseResponse SaveCategory(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<Category, Entity.Category>();

                if (!db.Categories.Any(e => e.Id == entityDb.Id))
                {
                    db.Categories.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse DeleteCategory(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Categories.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;

                    if (response.Success)
                    {
                        db.Database.ExecuteSqlCommand("DELETE CategoryImages WHERE CategoryId = {0}", id);
                    }
                }
                else
                {
                    response.Success = false;
                }
            }

            return response;
        }

        #region image

        public BaseResponse SaveImage(CategoryImage categoryImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = categoryImage.Map<CategoryImage, Entity.CategoryImage>();

                if (!db.CategoryImages.Any(e => e.Id == entityDb.Id))
                {
                    db.CategoryImages.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse SaveListImage(List<CategoryImage> listImage)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                if (listImage.Any())
                {
                    listImage.ForEach(categoryImage =>
                    {
                        var entityDb = categoryImage.Map<CategoryImage, Entity.CategoryImage>();
                        db.CategoryImages.Add(entityDb);

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
                var entity = db.CategoryImages.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public List<CategoryImage> GetImages(Guid categoryId)
        {
            using (var db = DbContext)
            {
                return
                    db.CategoryImages.Where(i => i.CategoryId == categoryId)
                        .ToList()
                        .MapList<CategoryImage>();
            }
        }

        public CategoryImage GetImage(Guid id)
        {
            using (var db = DbContext)
            {
                return db.CategoryImages.FirstOrDefault(i => i.Id == id).Map<Entity.CategoryImage, CategoryImage>();
            }
        }

        public BaseResponse UpdateImageInfo(CategoryImageUpdateInfo info)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = new Entity.CategoryImage { Id = info.Id };
                db.CategoryImages.Attach(entityDb);

                var changed = false;

                if (info.Visible.HasValue)
                {
                    entityDb.Visible = info.Visible.Value;
                    db.Entry(entityDb).Property(o => o.Visible).IsModified = true;
                    changed = true;
                }

                if (changed)
                {
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public bool CodeExists(string code, CategoryType categoryType)
        {
            using (var db = DbContext)
            {
                code = code.ToStr().Trim().ToLower();
                return db.Categories.Any(i => i.Code.Trim().ToLower() == code && i.CategoryType == categoryType);
            }
        }

        public bool CodeExists(string code, CategoryType categoryType, Guid id)
        {
            using (var db = DbContext)
            {
                code = code.ToStr().Trim().ToLower();
                return db.Categories.Any(i => i.Code.Trim().ToLower() == code
                    && i.CategoryType == categoryType
                    && i.Id != id
                    );
            }
        }

        #endregion
    }
}
