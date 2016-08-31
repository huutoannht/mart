using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.ContactUsDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "ContactUs")]
    public interface IContactUsService
    {
        [OperationContract]
        [FaultContract(typeof (ErrorManager))]
        BaseResponse SendEmail(ContactUs request);
    }
}
