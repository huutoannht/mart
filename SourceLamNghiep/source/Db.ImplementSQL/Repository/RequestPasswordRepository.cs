using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.RequestPasswordDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class RequestPasswordRepository : BaseRepository, IRequestPasswordRepository
    {
        public RequestPassword GetByEmail(string email)
        {
            using (var db = DbContext)
            {
                return db.RequestPasswords.FirstOrDefault(i => i.Email == email).Map<Entity.RequestPassword, RequestPassword>();
            }
        }

        public RequestPassword GetByRefKey(string refKey)
        {
            using (var db = DbContext)
            {
                return db.RequestPasswords.FirstOrDefault(i => i.KeyRef == refKey).Map<Entity.RequestPassword, RequestPassword>();
            }
        }

        public BaseResponse Save(SaveRequest request)
        {
            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<RequestPassword, Entity.RequestPassword>();

                entityDb.Email = entityDb.Email.ToStr().Trim().ToLower();

                if (!db.RequestPasswords.Any(e => e.Id == entityDb.Id))
                {
                    db.RequestPasswords.Add(entityDb);
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

        public bool Delete(Guid id)
        {
            using (var db = DbContext)
            {
                var entity = db.RequestPasswords.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
