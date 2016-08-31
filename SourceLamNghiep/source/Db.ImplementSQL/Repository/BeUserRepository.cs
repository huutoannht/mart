using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class BeUserRepository : BaseRepository, IBeUserRepository
    {
        public FindResponse FindBeUsers(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.BeUsers
                    .AsQueryable();

                if (!String.IsNullOrEmpty(request.TextSearch))
                {
                    request.TextSearch = request.TextSearch.ToStr().Trim().ToLower();
                    query = query.Where(q =>
                        q.Code.ToLower().Contains(request.TextSearch)
                        || q.FullName.ToLower().Contains(request.TextSearch)
                        || q.Email.ToLower().Contains(request.TextSearch)
                        || q.Phone.ToLower().Contains(request.TextSearch)
                        || q.Address.ToLower().Contains(request.TextSearch)
                        || q.Role.Name.ToLower().Contains(request.TextSearch)
                        || q.Position.Name.ToLower().Contains(request.TextSearch)
                        || q.Status.ToString().Contains(request.TextSearch)
                    );
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.FullName));

                var pagingResult = request.ApplyPageOption(query).ToList();
                
                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<BeUser>();
            }

            return response;
        }

        public List<BeUser> GetAll()
        {
            using (var db = DbContext)
            {
                return db.BeUsers.ToList().MapList<BeUser>();
            }
        }

        public BeUser GetBeUser(Guid id)
        {
            using (var db = DbContext)
            {
                return db.BeUsers.FirstOrDefault(u=> u.Id == id).Map<Entity.BeUser, BeUser>();
            }
        }

        public BaseResponse ChangePwd(ChangePwdResquest request)
        {
            using (var db = DbContext)
            {
                var entityDb = new Entity.BeUser { Id = request.UserId, HashPassword = request.NewPassword };
                db.BeUsers.Attach(entityDb);
                db.Entry(entityDb).Property(o => o.HashPassword).IsModified = true;
                db.SaveChanges();
            }

            return new BaseResponse();
        }

        public BaseResponse UpdateUserLogin(UpdateUserLoginRequest request)
        {
            var res = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = new Entity.BeUser { Id = request.UserId, LastLoginDate = DateTime.UtcNow };
                db.BeUsers.Attach(entityDb);
                db.Entry(entityDb).Property(o => o.LastLoginDate).IsModified = true;

                res.Success = db.SaveChanges() > 0;
            }

            return res;
        }

        public BaseResponse SaveBeUser(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<BeUser, Entity.BeUser>();

                if (!db.BeUsers.Any(e => e.Id == entityDb.Id))
                {
                    db.BeUsers.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;
            }

            return response;
        }

        public bool EmailExists(string email)
        {
            var emailName = email.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.BeUsers.Any(i => i.Email == emailName);
            }
        }

        public bool EmailExists(string email, Guid id)
        {
            var emailName = email.ToStr().Trim().ToLower();
            using (var db = DbContext)
            {
                return db.BeUsers.Any(i => i.Email == emailName && i.Id != id);
            }
        }

        public bool CodeExists(string code)
        {
            code = code.ToStr().ToLower().Trim();
            using (var db = DbContext)
            {
                return db.BeUsers.Any(i => i.Code.ToLower().Trim() == code);
            }
        }

        public bool CodeExists(string code, Guid id)
        {
            code = code.ToStr().ToLower().Trim();
            using (var db = DbContext)
            {
                return db.BeUsers.Any(i => i.Code.ToLower().Trim() == code && i.Id != id);
            }
        }

        public bool RoleIsUsed(Guid roleId)
        {
            using (var db = DbContext)
            {
                return db.BeUsers.Any(i => i.RoleId.HasValue && i.RoleId == roleId);
            }
        }

        public BaseResponse Delete(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.BeUsers.FirstOrDefault(e => e.Id == id);

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
