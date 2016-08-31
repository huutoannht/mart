using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class SaveRequest : BaseSaveRequest<Customer>
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsUpdateProfile { get; set; }

        [DataMember]
        public string Link { get; set; }
    }
}
