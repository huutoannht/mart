using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.DataLogDC
{
    [Serializable]
    [DataContract]
    public class DataLog : BaseDC
    {
        [DataMember]
        public TableLog Table { get; set; }

        [DataMember]
        public ActionLog Action { get; set; }

        [DataMember]
        public Guid BeUserId { get; set; }

        [DataMember]
        public string OldData { get; set; }

        [DataMember]
        public string NewData { get; set; }

        [DataMember]
        public DateTime LogDate { get; set; }

        [DataMember]
        public Guid ItemId { get; set; }
    }
}
