using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.ArticeDC;

namespace Db.Interfaces
{
    public interface IArticeRepository
    {
        FindResponse FindArtices(FindRequest request);

        Artice GetArtice(Guid id);

        BaseResponse SaveArtice(SaveRequest request);

        BaseResponse DeleteArtice(Guid id);

        BaseResponse UpdateInfo(UpdateInfo info);
    }
}
