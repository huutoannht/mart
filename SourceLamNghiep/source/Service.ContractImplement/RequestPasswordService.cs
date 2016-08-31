using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.RequestPasswordDC;
using Db.Interfaces;
using ML.Common;
using Service.Contract;
using Share.Helper;
using Share.Helper.Cache;
using StructureMap;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RequestPasswordService : BaseService, IRequestPasswordService
    {
        private readonly IRequestPasswordRepository _repository;
        private readonly ICustomerRepository _userRepository;
        private readonly ICacheHelper _cacheHelper;

        public RequestPasswordService(IRequestPasswordRepository repository, ICustomerRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _cacheHelper = ObjectFactory.GetInstance<ICacheHelper>();
        }

        public RequestPassword GetByEmail(string email)
        {
            return Execute(_repository, r => r.GetByEmail(email));
        }

        public RequestPassword GetByRefKey(string refKey)
        {
            return Execute(_repository, r => r.GetByRefKey(refKey));
        }

        public BaseResponse Save(SaveRequest request)
        {
            return Execute(_repository, r => r.Save(request));
        }

        public bool Delete(Guid id)
        {
            return Execute(_repository, r => r.Delete(id));
        }

        public BaseResponse SubmitRequestPassword(SubmitPasswordRequest request)
        {
            return Execute(_repository, r =>
            {
                var response = new BaseResponse();

                var keyRef = (new RandomStringGenerator()).Generate(50, true, true, false, false);

                request.Email = request.Email.ToStr().Trim().ToLower();

                var preferLanguage = string.Empty;
                var fullName = string.Empty;

                if (request.IsBeUser)
                {
                    var beUser = _cacheHelper.GetAllBeUsers().FirstOrDefault(i => i.Email == request.Email);
                    if (beUser == null)
                    {
                        response.Success = false;
                        response.Messages.Add("UnableFindUser"); //Resource key
                        return response;
                    }

                    preferLanguage = beUser.PreferLanguage;
                    fullName = beUser.FullName;
                }
                else
                {
                    var customer = _userRepository.GetCustomerByEmail(request.Email);

                    if (customer == null)
                    {
                        response.Success = false;
                        response.Messages.Add("UnableFindUser"); //Resource key
                        return response;
                    }

                    fullName = customer.ClinicName;
                }
                
                var oldRequest = r.GetByEmail(request.Email);

                if (oldRequest == null)
                {
                    var saveRequest = new SaveRequest
                    {
                        Entity = new RequestPassword
                        {
                            CreatedDate = DateTime.UtcNow,
                            Email = request.Email,
                            KeyRef = keyRef
                        }
                    };

                    saveRequest.Entity.InitId();

                    response.Success = r.Save(saveRequest).Success;
                }
                else
                {
                    keyRef = oldRequest.KeyRef;
                }

                if (response.Success)
                {
                    #region send mail

                    var emailTemplate =
                        _cacheHelper.GetSystemEmailTemplates()
                            .FirstOrDefault(i => i.Type == SystemEmailTemplateType.BeForgotPassword
                            && i.Language == SiteUtils.GetDefaultLanguageIfNullOrEmpty(preferLanguage))
                            ?? _cacheHelper.GetSystemEmailTemplates()
                            .FirstOrDefault(i => i.Type == SystemEmailTemplateType.BeForgotPassword
                            && i.Language == SiteUtils.GetDefaultLanguageIfNullOrEmpty());
                    if (emailTemplate == null)
                    {
                        response.Success = false;
                        response.Messages.Add("EmailTemplateDataNotFound"); //Resource key
                    }
                    else
                    {
                        var mapKeys = new Dictionary<string, string>();
                        mapKeys.Add(ConstKeys.Email, request.Email);
                        mapKeys.Add(ConstKeys.FullName, fullName);

                        var link = request.Link.ToStr();
                        if (link.EndsWith("/"))
                        {
                            link += keyRef;
                        }
                        else
                        {
                            link += "/" + keyRef;
                        }

                        mapKeys.Add(ConstKeys.LinkRecoverPassword, link);

                        EmailUtils.Instance.SendEmail(request.Email, emailTemplate, mapKeys);
                    }

                    #endregion
                }

                return response;
            });
        }
    }
}
