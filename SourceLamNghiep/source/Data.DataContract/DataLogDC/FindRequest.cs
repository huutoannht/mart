using System;
using System.Runtime.Serialization;

namespace Data.DataContract.DataLogDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string TextSearch { get; set; }

        [DataMember]
        public TableLog? Table { get; set; }

        [DataMember]
        public Guid? ItemId { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }
    }
}
