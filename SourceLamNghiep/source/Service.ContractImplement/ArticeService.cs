using System;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.ArticeDC;
using Db.Interfaces;
using Service.Contract;
using Share.Helper.Cache;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ArticeService : BaseService, IArticeService
    {
        private readonly IArticeRepository _repository;
        private readonly ICacheHelper _cacheHelper;

        public ArticeService(IArticeRepository repository, ICacheHelper cacheHelper)
        {
            _repository = repository;
            _cacheHelper = cacheHelper;
        }

        public FindResponse FindArtices(FindRequest request)
        {
            return Execute(_repository, r => r.FindArtices(request));
        }

        public Artice GetArtice(Guid id)
        {
            return Execute(_repository, r => r.GetArtice(id));
        }

        public BaseResponse SaveArtice(SaveRequest request)
        {
            return Execute(_repository, r => r.SaveArtice(request));
        }

        public BaseResponse DeleteArtice(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var entity = r.GetArtice(id);

                var res = r.DeleteArtice(id);

                if (res.Success && entity != null)
                {
                    DeleteFile(dataFolderPath, entity.ImagePath);
                }

                return res;
            });
        }

        public BaseResponse UpdateInfo(UpdateInfo info)
        {
            return Execute(_repository, r => r.UpdateInfo(info));
        }
    }
}
