using System;
using System.Collections.Generic;
using Data.DataContract;
using Data.DataContract.SystemEmailTemplateDC;

namespace Db.Interfaces
{
    public interface ISystemEmailTemplateRepository
    {
        SystemEmailTemplate GetSystemEmailTemplate(Guid id);

        BaseResponse SaveSystemEmailTemplate(SaveRequest request);

        SystemEmailTemplate GetSystemEmailTemplate(SystemEmailTemplateType type, string language);

        List<SystemEmailTemplate> GetAll();
    }
}
