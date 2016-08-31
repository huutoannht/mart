using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.FileDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "File")]
    public interface IFileService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        bool CheckNameExisted(string name);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        bool CheckNameExisted2(string name, Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        File GetFile(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindFiles(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveFile(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteFile(Guid id, string dataFolderPath);
    }
}
