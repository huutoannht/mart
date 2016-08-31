using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract.ArticeDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string TextSearch { get; set; }
    }
}
