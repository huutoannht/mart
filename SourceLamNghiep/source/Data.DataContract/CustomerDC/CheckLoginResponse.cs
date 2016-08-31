using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class CheckLoginResponse : BaseResponse
    {
        [DataMember]
        public Customer User { get; set; }
    }
}
