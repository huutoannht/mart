using System;
using System.Runtime.Serialization;
using Data.DataContract;

namespace Data.DataContract.RequestPasswordDC
{
    [DataContract]
    [Serializable]
    public class RequestPassword : BaseDC
    {
        [DataMember]
        public string KeyRef { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string ExternalId { get; set; }
    }
}
