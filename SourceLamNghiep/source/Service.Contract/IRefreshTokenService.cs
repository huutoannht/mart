using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.RefreshTokenDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "RefreshToken")]
    public interface IRefreshTokenService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        RefreshToken GetRefreshToken(string id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse Save(RefreshToken entity);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse Delete(string id);
    }
}
