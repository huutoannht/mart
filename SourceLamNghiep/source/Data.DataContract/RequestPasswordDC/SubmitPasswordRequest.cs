using System;
using System.Runtime.Serialization;

namespace Data.DataContract.RequestPasswordDC
{
    [Serializable]
    [DataContract]
    public class SubmitPasswordRequest
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public bool IsBeUser { get; set; }
    }
}
