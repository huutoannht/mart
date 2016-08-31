using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.DataContract;
using Data.DataContract.RoleDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public Role GetRole(Guid id)
        {
            using (var db = DbContext)
            {
                return db.Roles.FirstOrDefault(u => u.Id == id).Map<Entity.Role, Role>();
            }
        }

        public BaseResponse SaveRole(SaveRequest request)
        {
            var response = new BaseResponse();

            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<Role, Entity.Role>();
                var permissions = entityDb.Permissions;
                entityDb.Permissions = null;

                if (!db.Roles.Any(e => e.Id == entityDb.Id))
                {
                    db.Roles.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                response.Success = db.SaveChanges() > 0;

                if (response.Success)
                {
                    var roleId = (permissions.FirstOrDefault() ?? new Entity.RolePermission()).RoleId;

                    db.Database.ExecuteSqlCommand("DELETE RolePermissions WHERE RoleId = {0}", roleId);

                    foreach (var permission in permissions)
                    {
                        db.RolePermissions.Add(permission);
                    }

                    response.Success = db.SaveChanges() > 0;
                }
            }

            return response;
        }

        public FindResponse FindRoles(FindRequest request)
        {
            var response = new FindResponse();

            using (var db = DbContext)
            {
                var query = db.Roles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    request.Name = request.Name.ToStr().Trim().ToLower();
                    query = query.Where(i => i.Name.ToLower().Contains(request.Name)
                        || i.Description.ToLower().Contains(request.Name));
                }

                query = request.IsSortingValid ? request.ApplySortOption(query) : request.ApplyPageOption(query.OrderBy(q => q.Name));

                var pagingResult = request.ApplyPageOption(query).ToList();

                response.TotalRecords = query.Count();
                response.Results = pagingResult.MapList<Role>();
            }

            return response;
        }

        public BaseResponse DeleteRole(Guid id)
        {
            var response = new BaseResponse();
            using (var db = DbContext)
            {
                var entity = db.Roles.FirstOrDefault(e => e.Id == id);

                if (entity != null)
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    response.Success = db.SaveChanges() > 0;

                    if (response.Success)
                    {
                        db.Database.ExecuteSqlCommand("DELETE RolePermissions WHERE RoleId = {0}", id);
                    }
                }
                else
                {
                    response.Success = false;
                }
            }

            return response;
        }

        public List<RolePermission> GetPermissions(Guid roleId)
        {
            using (var db = DbContext)
            {
                return db.RolePermissions.Where(i => i.RoleId == roleId).ToList().MapList<RolePermission>();
            }
        }

        public bool NameIsExist(string name)
        {
            using (var db = DbContext)
            {
                return db.Roles.Any(i => i.Name == name);
            }
        }

        public bool NameIsExist(string name, Guid id)
        {
            using (var db = DbContext)
            {
                return db.Roles.Any(i => i.Name == name && i.Id != id);
            }
        }

        public bool IsExists(Guid id)
        {
            using (var db = DbContext)
            {
                return db.Roles.Any(i => i.Id == id);
            }
        }

        public List<Role> GetAll()
        {
            using (var db = DbContext)
            {
                return db.Roles.ToList().MapList<Role>();
            }
        }
    }
}
