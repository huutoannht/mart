using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "SystemEmailTemplate")]
    public interface ISystemEmailTemplateService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        SystemEmailTemplate GetSystemEmailTemplateById(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveSystemEmailTemplate(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        SystemEmailTemplate GetSystemEmailTemplateByTypeAndLanguage(SystemEmailTemplateType type, string language);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<SystemEmailTemplate> GetAll();
    }
}
