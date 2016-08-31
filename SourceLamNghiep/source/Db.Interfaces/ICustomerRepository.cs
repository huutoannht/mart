using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.CustomerDC;

namespace Db.Interfaces
{
    public interface ICustomerRepository
    {
        BaseResponse SaveCustomers(List<Customer> customers);

        BaseResponse DeleteCustomer(Guid id);

        BaseResponse UpdateInfo(Customer user, UpdateInfo info);

        FindResponse FindCustomers(FindRequest request);

        Customer GetCustomer(Guid id);

        Customer GetCustomer(string clinicId);

        BaseResponse SaveCustomer(SaveRequest request);

        Customer GetCustomerByEmail(string email);

        bool ClinicIdIsExisted(string clinicId);

        bool ClinicIdIsExisted(string clinicId, Guid id);

        List<string> GetAllEmails();

        #region images

        BaseResponse SaveImage(CustomerImage customerImage);

        BaseResponse SaveListImage(List<CustomerImage> listImage);

        BaseResponse DeleteImage(Guid id);

        BaseResponse DeleteImages(List<Guid> ids);

        List<CustomerImage> GetImages(Guid customerId);

        List<CustomerImage> GetImages(List<Guid> ids);

        CustomerImage GetImage(Guid id);

        #endregion
    }
}
