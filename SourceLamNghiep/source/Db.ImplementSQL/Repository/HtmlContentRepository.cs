using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.HtmlContentDC;
using Db.Interfaces;
using ML.Common;

namespace Db.ImplementSQL.Repository
{
    public class HtmlContentRepository : BaseRepository, IHtmlContentRepository
    {
        public List<HtmlContent> GetAll()
        {
            using (var db = DbContext)
            {
                var list = db.HtmlContents.ToList();
                return list.MapList<HtmlContent>();
            }
        }

        public HtmlContent GetHtmlContent(short type, HtmlContentDisplayType htmlContentDisplayType, string languageCode)
        {
            using (var db = DbContext)
            {
                var ent = db.HtmlContents.FirstOrDefault(u => u.Type == type 
                    && u.LanguageCode == languageCode
                    && u.HtmlContentDisplayType == htmlContentDisplayType);
                return ent.Map<Entity.HtmlContent, HtmlContent>();
            }
        }

        public BaseResponse SaveHtmlContent(SaveRequest request)
        {
            using (var db = DbContext)
            {
                var entityDb = request.Entity.Map<HtmlContent, Entity.HtmlContent>();

                if (!db.HtmlContents.Any(e => e.Id == entityDb.Id))
                {
                    db.HtmlContents.Add(entityDb);
                }
                else
                {
                    db.Entry(entityDb).State = EntityState.Modified;
                }

                var success = db.SaveChanges() > 0;
                return new BaseResponse
                {
                    Success = success
                };
            }
        }
    }
}
