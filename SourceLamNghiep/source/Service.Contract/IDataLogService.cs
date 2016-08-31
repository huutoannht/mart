using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.DataLogDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "DataLog")]
    public interface IDataLogService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse Insert(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindDataLogs(FindRequest request);
    }
}
