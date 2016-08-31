using System;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.AppSettingDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AppSettingService : BaseService, IAppSettingService
    {
        private readonly IAppSettingRepository _repository;

        public AppSettingService(IAppSettingRepository repository)
        {
            _repository = repository;
        }

        public bool Delete(Guid id)
        {
            return Execute(_repository, r => r.Delete(id));
        }

        public AppSetting GetAppSetting(Guid id)
        {
            return Execute(_repository, r => r.GetAppSetting(id));
        }

        public FindResponse FindAppSettings(FindRequest request)
        {
            return Execute(_repository, r => r.FindAppSettings(request));
        }

        public BaseResponse SaveAppSetting(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                if (request.Entity.IsNew && r.CheckExist(request.Entity.SettingType, request.Entity.Name))
                {
                    response.Success = false;
                    response.Messages.Add("NameIsExisted"); //resource key
                    return response;
                }

                if (!request.Entity.IsNew && r.CheckExist(request.Entity.SettingType, request.Entity.Name, request.Entity.Id))
                {
                    response.Success = false;
                    response.Messages.Add("NameIsExisted"); //resource key
                    return response;
                }

                if (request.Entity.IsNew)
                {
                    request.Entity.InitId();
                }

                r.SaveAppSetting(request);

                return response;
            });
        }

    }
}
