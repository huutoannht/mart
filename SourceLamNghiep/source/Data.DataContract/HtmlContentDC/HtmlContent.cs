using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Data.DataContract.HtmlContentDC
{
    [Serializable]
    [DataContract]
    public class HtmlContent : BaseDC
    {
        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public short Type { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public HtmlContentDisplayType HtmlContentDisplayType { get; set; }
    }
}
