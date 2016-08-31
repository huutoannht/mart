using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class HtmlContent : BaseEntity
    {
        public string LanguageCode { get; set; }

        public short Type { get; set; }

        public string Content { get; set; }

        public string Header { get; set; }

        public HtmlContentDisplayType HtmlContentDisplayType { get; set; }
    }
}
