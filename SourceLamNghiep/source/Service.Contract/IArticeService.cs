using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.ArticeDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "Artice")]
    public interface IArticeService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindArtices(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        Artice GetArtice(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveArtice(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteArtice(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateInfo(UpdateInfo info);
    }
}
