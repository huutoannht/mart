using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.DataLogDC;

namespace Db.Interfaces
{
    public interface IDataLogRepository
    {
        BaseResponse Insert(SaveRequest request);

        FindResponse FindDataLogs(FindRequest request);
    }
}
