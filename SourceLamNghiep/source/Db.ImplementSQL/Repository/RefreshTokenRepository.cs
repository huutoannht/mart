using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.RefreshTokenDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository
    {
        public RefreshToken GetRefreshToken(string id)
        {
            using (var db = DbContext)
            {
                return db.RefreshTokens.FirstOrDefault(u => u.Id == id).Map<Entity.RefreshToken, RefreshToken>();
            }
        }

        public BaseResponse Save(RefreshToken entity)
        {
            using (var db = DbContext)
            {
                var entityDb = entity.Map<RefreshToken, Entity.RefreshToken>();

                if (!db.RefreshTokens.Any(e => e.Id == entityDb.Id))
                {
                    db.RefreshTokens.Add(entityDb);
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

        public BaseResponse Delete(string id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.RefreshTokens.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;
                }
                else
                {
                    response.Success = false;
                }
            }

            return response;
        }
    }
}
