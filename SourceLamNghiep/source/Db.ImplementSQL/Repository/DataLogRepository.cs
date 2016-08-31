using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.DataLogDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class DataLogRepository : BaseRepository, IDataLogRepository
    {
        public BaseResponse Insert(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<DataLog, Entity.DataLog>();

                db.DataLogs.Add(entityDb);

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public FindResponse FindDataLogs(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.DataLogs.AsQueryable();

                if (request.Table.HasValue)
                {
                    query = query.Where(q => q.Table == request.Table);
                }

                if (request.ItemId.HasValue)
                {
                    query = query.Where(q => q.ItemId == request.ItemId);
                }

                if (!String.IsNullOrEmpty(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.ToStr().Trim().ToLower();
                    query = query.Where(q => 
                        q.BeUser.FullName.ToLower().Contains(request.TextSearch)
                        || q.OldData.ToLower().Contains(request.TextSearch)
                        || q.NewData.ToLower().Contains(request.TextSearch)
                        || q.Action.ToString().ToLower().Contains(request.TextSearch)
                        );
                }

                if (request.FromDate.HasValue)
                {
                    query = query.Where(q => q.LogDate >= request.FromDate);
                }

                if (request.ToDate.HasValue)
                {
                    query = query.Where(q => q.LogDate <= request.ToDate);
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderByDescending(q => q.LogDate));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<DataLog>();
            }

            return response;
        }
    }
}
