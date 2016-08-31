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
    public class SaveRequest : BaseSaveRequest<BeUser>
    {
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsUpdateProfile { get; set; }
    }
}
