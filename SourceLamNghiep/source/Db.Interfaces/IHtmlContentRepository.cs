using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.HtmlContentDC;

namespace Db.Interfaces
{
    public interface IHtmlContentRepository
    {
        List<HtmlContent> GetAll();

        HtmlContent GetHtmlContent(short type, HtmlContentDisplayType htmlContentDisplayType, string languageCode);

        BaseResponse SaveHtmlContent(SaveRequest request);
    }
}
