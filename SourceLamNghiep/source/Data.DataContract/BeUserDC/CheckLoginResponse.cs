using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.BeUserDC
{
    [Serializable]
    [DataContract]
    public class CheckLoginResponse : BaseResponse
    {
        [DataMember]
        public BeUser BeUser { get; set; }
    }
}
