using System.Collections.Generic;
using System.ServiceModel.Activation;
using Data.DataContract;
using Data.DataContract.HtmlContentDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HtmlContentService : BaseService, IHtmlContentService
    {
        private readonly IHtmlContentRepository _repository;

        public HtmlContentService(IHtmlContentRepository repository)
        {
            _repository = repository;
        }

        public HtmlContent GetHtmlContent(short type, HtmlContentDisplayType htmlContentDisplayType, string languageCode)
        {
            return Execute(_repository, r => r.GetHtmlContent(type, htmlContentDisplayType, languageCode));
        }

        public BaseResponse SaveHtmlContent(SaveRequest request)
        {
            return Execute(_repository, r => r.SaveHtmlContent(request));
        }

        public List<HtmlContent> GetAll()
        {
            return Execute(_repository, r => r.GetAll());
        }
    }
}
