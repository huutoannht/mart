using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.RefreshTokenDC;

namespace Db.Interfaces
{
    public interface IRefreshTokenRepository
    {
        RefreshToken GetRefreshToken(string id);

        BaseResponse Save(RefreshToken entity);

        BaseResponse Delete(string id);
    }
}
