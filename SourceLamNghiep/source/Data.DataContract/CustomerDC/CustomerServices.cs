using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class CustomerServices : BaseDC
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public DateTime DateCS { get; set; }

        [DataMember]
        public Guid BeUserId { get; set; }

        [DataMember]
        public string Promise { get; set; }

        [DataMember]
        public YesNo Done { get; set; }
    }
}
