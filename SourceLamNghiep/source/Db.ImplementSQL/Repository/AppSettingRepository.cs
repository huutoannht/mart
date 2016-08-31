using System;
using System.Data.Entity;
using System.Linq;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class AppSettingRepository : BaseRepository, IAppSettingRepository
    {
        public bool Delete(Guid id)
        {
            using (var db = DbContext)
            {
                var entity = db.AppSettings.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool CheckExist(AppSettingType settingType, string name)
        {
            using (var db = DbContext)
            {
                return db.AppSettings.Any(i => i.SettingType == settingType && i.Name.ToLower() == name.ToLower());
            }
        }

        public bool CheckExist(AppSettingType settingType, string name, Guid id)
        {
            using (var db = DbContext)
            {
                return db.AppSettings.Any(i => i.SettingType == settingType && i.Name.ToLower() == name.ToLower()
                    && i.Id != id);
            }
        }

        public AppSetting GetAppSetting(Guid id)
        {
            using (var db = DbContext)
            {
                return db.AppSettings.FirstOrDefault(u => u.Id == id).Map<Entity.AppSetting, AppSetting>();
            }
        }

        public FindResponse FindAppSettings(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.AppSettings.AsQueryable();

                if (request.SettingType.HasValue)
                {
                    query = query.Where(i => i.SettingType == request.SettingType);
                }

                if (request.ExcludeSettingTypes != null && request.ExcludeSettingTypes.Any())
                {
                    query = query.Where(i => request.ExcludeSettingTypes.All(k => k != i.SettingType));
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<AppSetting>();
            }

            return response;
        }

        public BaseResponse SaveAppSetting(SaveRequest request)
        {
            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<AppSetting, Entity.AppSetting>();

                if (!db.AppSettings.Any(e => e.Id == entityDb.Id))
                {
                    db.AppSettings.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                var success = db.SaveChanges() > 0;
                return new BaseResponse
                {
                    Success = success
                };
            }
        }
    }
}
