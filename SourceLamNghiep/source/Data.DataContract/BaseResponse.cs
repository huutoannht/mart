using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            Messages = new List<string>();
        }

        [DataMember]
        public Guid EntityId { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public List<string> Messages { get; set; }

        [DataMember]
        public bool MessageNoResource { get; set; }
    }
}
