using System;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.RequestPasswordDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "BeRequestPassword")]
    public interface IRequestPasswordService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        RequestPassword GetByEmail(string email);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        RequestPassword GetByRefKey(string refKey);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse Save(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        bool Delete(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SubmitRequestPassword(SubmitPasswordRequest request);
    }
}
