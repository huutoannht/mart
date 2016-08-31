using System;
using System.Collections.Generic;
using System.ServiceModel;
using Data.DataContract;
using Data.DataContract.CustomerDC;
using ML.Common.Error;

namespace Service.Contract
{
    [ServiceContract(Name = "Customer")]
    public interface ICustomerService
    {
        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveCustomers(List<Customer> customers);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteCustomer(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<string> GetAllEmails();

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse UpdateInfo(Customer user, UpdateInfo info);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        FindResponse FindCustomers(FindRequest request);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        Customer GetCustomer(Guid id);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveCustomer(SaveRequest request);

        #region images

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveImage(CustomerImage customerImage);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse SaveListImage(List<CustomerImage> listImage);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteImage(Guid id, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        BaseResponse DeleteImages(List<Guid> ids, string dataFolderPath);

        [OperationContract]
        [FaultContract(typeof(ErrorManager))]
        List<CustomerImage> GetImages(Guid customerId);

        #endregion
    }
}
