using System;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "AppSetting")]
    public interface IAppSettingService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        bool Delete(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        AppSetting GetAppSetting(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindAppSettings(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveAppSetting(SaveRequest request);
    }
}
