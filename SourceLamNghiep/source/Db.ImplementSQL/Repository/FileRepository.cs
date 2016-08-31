using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.FileDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        public bool CheckNameExisted(string name)
        {
            name = name.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.Files.Any(i => i.Name.Trim().ToLower() == name);
            }
        }

        public bool CheckNameExisted(string name, Guid id)
        {
            name = name.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.Files.Any(i => i.Name.Trim().ToLower() == name && i.Id != id);
            }
        }

        public File GetFile(Guid id)
        {
            using (var db = DbContext)
            {
                return db.Files.FirstOrDefault(i => i.Id == id).Map<Entity.File, File>();
            }
        }

        public FindResponse FindFiles(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Files
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.ToLower().Trim();
                    query = query.Where(i => 
                        i.Name.ToLower().Contains(request.TextSearch)
                        || i.BeUser.FullName.ToLower().Contains(request.TextSearch)
                        );
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<File>();
            }

            return response;
        }

        public BaseResponse SaveFile(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<File, Entity.File>();

                if (!db.Files.Any(e => e.Id == entityDb.Id))
                {
                    db.Files.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public BaseResponse DeleteFile(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Files.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }
    }
}
