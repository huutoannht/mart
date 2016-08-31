using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.DataLogDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataLogService : BaseService, IDataLogService
    {
        private readonly IDataLogRepository _repository;

        public DataLogService(IDataLogRepository repository)
        {
            _repository = repository;
        }

        public BaseResponse Insert(SaveRequest request)
        {
            return Execute(_repository, r =>
            {
                if (request.Entity.IsNew)
                {
                    request.Entity.InitId();
                }

                var res = r.Insert(request);
                return res;
            });
        }

        public FindResponse FindDataLogs(FindRequest request)
        {
            return Execute(_repository, r => r.FindDataLogs(request));
        }
    }
}
