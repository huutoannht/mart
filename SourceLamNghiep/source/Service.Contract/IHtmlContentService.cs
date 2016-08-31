using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.HtmlContentDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "HtmlContent")]
    public interface IHtmlContentService
    {
        [OperationContract]
        [FaultContract(typeof (ErrorManager))]
        HtmlContent GetHtmlContent(short type, HtmlContentDisplayType htmlContentDisplayType, string languageCode);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveHtmlContent(SaveRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<HtmlContent> GetAll();
    }
}
