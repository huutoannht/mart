using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class SystemEmailTemplateRepository : BaseRepository, ISystemEmailTemplateRepository
    {
        public SystemEmailTemplate GetSystemEmailTemplate(Guid id)
        {
            using (var db = DbContext)
            {
                return db.SystemEmailTemplates.FirstOrDefault(u => u.Id == id).Map<Entity.SystemEmailTemplate, SystemEmailTemplate>();
            }
        }

        public BaseResponse SaveSystemEmailTemplate(SaveRequest request)
        {
            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<SystemEmailTemplate, Entity.SystemEmailTemplate>();

                if (!db.SystemEmailTemplates.Any(e => e.Id == entityDb.Id))
                {
                    db.SystemEmailTemplates.Add(entityDb);
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

        public SystemEmailTemplate GetSystemEmailTemplate(SystemEmailTemplateType type, string language)
        {
            using (var db = DbContext)
            {
                return db.SystemEmailTemplates
                    .FirstOrDefault(u => u.Type == type && u.Language == language)
                    .Map<Entity.SystemEmailTemplate, SystemEmailTemplate>();
            }
        }

        public List<SystemEmailTemplate> GetAll()
        {
            using (var db = DbContext)
            {
                return db.SystemEmailTemplates.ToList().MapList<SystemEmailTemplate>();
            }
        }
    }
}
