using System;
using System.Linq;
using Data.DataContract;
using Data.DataContract.ArticeDC;
using Db.Interfaces;
using ML.Common;
using System.Data.Entity;

namespace Db.ImplementSQL.Repository
{
    public class ArticeRepository : BaseRepository, IArticeRepository
    {
        public FindResponse FindArtices(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Artices
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.ToLower().Trim();
                    query = query.Where(q => q.Name.ToLower().Contains(request.TextSearch)
                        || q.Description.ToLower().Contains(request.TextSearch)
                        || q.Content.ToLower().Contains(request.TextSearch)
                        || q.Status.ToString().ToLower().Contains(request.TextSearch)
                        );
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<Artice>();
            }

            return response;
        }

        public Artice GetArtice(Guid id)
        {
            using (var db = DbContext)
            {
                var query = db.Artices
                    .AsQueryable();

                return query.FirstOrDefault(i => i.Id == id).Map<Entity.Artice, Artice>();
            }
        }

        public BaseResponse SaveArtice(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<Artice, Entity.Artice>();

                if (!db.Artices.Any(e => e.Id == entityDb.Id))
                {
                    db.Artices.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse DeleteArtice(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Artices.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public BaseResponse UpdateInfo(UpdateInfo info)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = new Entity.Artice { Id = info.Id };
                db.Artices.Attach(entityDb);

                var changed = false;

                if (info.Status.HasValue)
                {
                    entityDb.Status = info.Status.Value;
                    db.Entry(entityDb).Property(o => o.Status).IsModified = true;
                    changed = true;
                }

                if (changed)
                {
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }
    }
}
