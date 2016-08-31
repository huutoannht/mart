using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.ContactUsDC;
using Db.Interfaces;
using ML.Common;
using Service.Contract;
using Share.Helper;
using Share.Helper.Cache;
using StructureMap;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContactUsService : BaseService, IContactUsService
    {
        private readonly ISystemEmailTemplateRepository _repository;

        private readonly ICacheHelper _cacheHelper;

        public ContactUsService(ISystemEmailTemplateRepository repository)
        {
            _repository = repository;

            _cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
        }

        public BaseResponse SendEmail(ContactUs request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var emailTemplate =
                       _cacheHelper.GetSystemEmailTemplates()
                           .FirstOrDefault(i => i.Type == SystemEmailTemplateType.StoreFrontContactUs
                           && i.Language == SiteUtils.GetDefaultLanguageIfNullOrEmpty(SiteUtils.GetAdminNotifyLanguage()))
                           ?? _cacheHelper.GetSystemEmailTemplates()
                           .FirstOrDefault(i => i.Type == SystemEmailTemplateType.StoreFrontContactUs
                           && i.Language == SiteUtils.GetDefaultLanguageIfNullOrEmpty());

                if (emailTemplate == null)
                {
                    response.Success = false;
                    response.Messages.Add("EmailTemplateDataNotFound"); //Resource key
                }
                else
                {
                    emailTemplate.Subject = emailTemplate.Subject.ToStr() + " - " + request.Subject;

                    var mapKeys = new Dictionary<string, string>();
                    mapKeys.Add(ConstKeys.Email, request.Email);
                    mapKeys.Add(ConstKeys.Name, request.Name);
                    mapKeys.Add(ConstKeys.Phone, request.Phone);
                    mapKeys.Add(ConstKeys.Subject, request.Subject);
                    mapKeys.Add(ConstKeys.Message, request.Message);

                    var emailNotification = SiteUtils.GetAdminEmailNotification();

                    if (string.IsNullOrWhiteSpace(emailNotification) || !std.IsEmail(emailNotification))
                    {
                        response.Success = false;
                        response.Messages.Add("EmailNotificationIsNotSetup"); //Resource key
                    }
                    else
                    {
                        EmailUtils.Instance.SendEmail(emailNotification, emailTemplate, mapKeys);
                    }
                }

                return response;
            });
        }
    }
}
