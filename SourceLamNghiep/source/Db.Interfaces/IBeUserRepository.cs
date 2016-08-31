using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.BeUserDC;

namespace Db.Interfaces
{
    public interface IBeUserRepository
    {
        FindResponse FindBeUsers(FindRequest request);

        List<BeUser> GetAll();

        BeUser GetBeUser(Guid id);

        BaseResponse ChangePwd(ChangePwdResquest request);

        BaseResponse UpdateUserLogin(UpdateUserLoginRequest request);

        BaseResponse SaveBeUser(SaveRequest request);

        bool EmailExists(string email);

        bool EmailExists(string email, Guid id);

        bool CodeExists(string code);

        bool CodeExists(string code, Guid id);

        bool RoleIsUsed(Guid roleId);

        BaseResponse Delete(Guid id);
    }
}
