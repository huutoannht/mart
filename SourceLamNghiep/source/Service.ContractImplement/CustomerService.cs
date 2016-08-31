using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.CustomerDC;
using Db.Interfaces;
using ML.Common;
using Service.Contract;
using Share.Helper.Cache;
using Share.Helper.Extension;
using Share.Messages.ServiceMessage;
using SaveRequest = Data.DataContract.CustomerDC.SaveRequest;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICacheHelper _cacheHelper;

        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository
            , ICacheHelper cacheHelper)
        {
            _cacheHelper = cacheHelper;
            _repository = repository;
        }

        public List<string> GetAllEmails()
        {
            return Execute(_repository, r => r.GetAllEmails());
        }

        public BaseResponse UpdateInfo(Customer user, UpdateInfo info)
        {
            return Execute(_repository, r =>
            {
                _cacheHelper.ClearGetCustomer(user.Id);

                return r.UpdateInfo(user, info);
            });
        }

        public FindResponse FindCustomers(FindRequest request)
        {
            return Execute(_repository, r => r.FindCustomers(request));
        }

        public Customer GetCustomer(Guid id)
        {
            return Execute(_repository, r => r.GetCustomer(id));
        }

        public CheckLoginResponse CheckLogin(CheckLoginRequest request)
        {
            return Execute(_repository, r =>
            {
                request.Email = request.Email.ToStr().Trim().ToLower();
                var response = new CheckLoginResponse
                {
                    User = _repository.GetCustomerByEmail(request.Email)
                };

                if (response.User == null)
                {
                    response.Messages.Add("UnableFindUser");//Return resouce key
                    return response;
                }

                response.EntityId = response.User.Id;

                return response;
            });
        }

        public BaseResponse SaveCustomer(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var user = request.Entity;
                user.ClinicEmail = user.ClinicEmail.ToStr().Trim().ToLower();

                #region check

                if (user.IsNew && _repository.ClinicIdIsExisted(user.ClinicId))
                {
                    response.Success = false;
                    response.Messages.Add("ClinicIdExisted");//res key
                    return response;
                }

                if (!user.IsNew && _repository.ClinicIdIsExisted(user.ClinicId, user.Id))
                {
                    response.Success = false;
                    response.Messages.Add("ClinicIdExisted");
                    return response;
                }
              
                #endregion

                response = r.SaveCustomer(request);

                if (response.Success)
                {
                    _cacheHelper.ClearGetCustomer(user.Id);
                }

                response.EntityId = user.Id;

                return response;
            });
        }

        public BaseResponse SaveCustomers(List<Customer> customers)
        {
            return Execute(_repository, r =>
            {
                var res = new BaseResponse();

                var rowIndex = 2;
                customers.ForEach(customer =>
                {
                    if (r.ClinicIdIsExisted(customer.ClinicId))
                    {
                        var cust = r.GetCustomer(customer.ClinicId);
                        res.Messages.Add(
                            string.Format(ServiceMessage.ExcelFileClinicIdExisted, 
                            rowIndex,
                            customer.ClinicId + (cust != null ? string.Format(" ({0})", cust.CustomerType.GetEnumDesRes()) : string.Empty))
                            );
                    }
                    ++rowIndex;
                });

                if (res.Messages.Any())
                {
                    res.Success = false;
                    return res;
                }

                res = r.SaveCustomers(customers);

                return res;
            });
        }

        public BaseResponse DeleteCustomer(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var customer = r.GetCustomer(id);

                var res = new BaseResponse();

                if (customer == null) return res;

                res = r.DeleteCustomer(id);

                if (res.Success)
                {
                    customer.Images.ForEach(image => DeleteFile(dataFolderPath, image.ImagePath));
                }

                return res;
            });
        }

        public BaseResponse SaveImage(CustomerImage customerImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveImage(customerImage);
                return res;
            });
        }

        public BaseResponse SaveListImage(List<CustomerImage> listImage)
        {
            return Execute(_repository, r =>
            {
                var res = r.SaveListImage(listImage);
                return res;
            });
        }

        public BaseResponse DeleteImage(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {

                var res = new BaseResponse();

                var image = r.GetImage(id);

                if (image == null)
                {
                    res.Success = false;
                    res.Messages.Add("DataNotFound"); //res key
                    return res;
                }

                res = r.DeleteImage(id);

                if (res.Success)
                {
                    DeleteFile(dataFolderPath, image.ImagePath);
                }

                return res;
            });
        }

        public BaseResponse DeleteImages(List<Guid> ids, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var images = r.GetImages(ids);

                var res = r.DeleteImages(ids);

                if (res.Success)
                {
                    images.ForEach(image => DeleteFile(dataFolderPath, image.ImagePath));
                }

                return res;
            });
        }

        public List<CustomerImage> GetImages(Guid customerId)
        {
            return Execute(_repository, r => r.GetImages(customerId));
        }
    }
}
