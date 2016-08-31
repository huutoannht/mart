using System;
using System.Runtime.Serialization;

namespace Data.DataContract.BeUserDC
{
    [Serializable]
    [DataContract]
    public class ChangePwdResquest
    {
        [DataMember]
        public string KeyRef { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string NewPassword { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
