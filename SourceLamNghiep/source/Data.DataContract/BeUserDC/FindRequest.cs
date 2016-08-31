using System;
using System.Runtime.Serialization;

namespace Data.DataContract.BeUserDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string TextSearch { get; set; }
    }
}
