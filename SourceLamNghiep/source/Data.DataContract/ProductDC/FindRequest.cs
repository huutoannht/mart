using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Data.DataContract.ProductDC
{
    [Serializable]
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string TextSearch { get; set; }
    }
}
