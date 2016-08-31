using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.RoleDC;

namespace Db.Interfaces
{
    public interface IRoleRepository
    {
        Role GetRole(Guid id);

        BaseResponse SaveRole(SaveRequest request);

        FindResponse FindRoles(FindRequest request);

        BaseResponse DeleteRole(Guid id);

        List<RolePermission> GetPermissions(Guid roleId);

        bool NameIsExist(string name);

        bool NameIsExist(string name, Guid id);

        bool IsExists(Guid id);

        List<Role> GetAll();
    }
}
