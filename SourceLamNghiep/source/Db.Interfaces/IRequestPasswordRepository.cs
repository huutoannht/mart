using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.RequestPasswordDC;

namespace Db.Interfaces
{
    public interface IRequestPasswordRepository
    {
        RequestPassword GetByEmail(string email);

        RequestPassword GetByRefKey(string refKey);

        BaseResponse Save(SaveRequest request);

        bool Delete(Guid id);
    }
}
