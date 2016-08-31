using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SystemEmailTemplateService : BaseService, ISystemEmailTemplateService
    {
        private readonly ISystemEmailTemplateRepository _repository;

        public SystemEmailTemplateService(ISystemEmailTemplateRepository repository)
        {
            _repository = repository;
        }

        public SystemEmailTemplate GetSystemEmailTemplateById(Guid id)
        {
            return Execute(_repository, r => r.GetSystemEmailTemplate(id));
        }

        public BaseResponse SaveSystemEmailTemplate(SaveRequest request)
        {
            return Execute(_repository, r => r.SaveSystemEmailTemplate(request));
        }

        public SystemEmailTemplate GetSystemEmailTemplateByTypeAndLanguage(SystemEmailTemplateType type, string language)
        {
            return Execute(_repository, r => r.GetSystemEmailTemplate(type, language));
        }

        public List<SystemEmailTemplate> GetAll()
        {
            return Execute(_repository, r => r.GetAll());
        }
    }
}
