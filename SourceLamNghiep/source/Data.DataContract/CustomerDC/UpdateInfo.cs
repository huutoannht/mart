using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class UpdateInfo
    {
        [DataMember]
       public CustomerType? CustomerType { get; set; }
    }
}
