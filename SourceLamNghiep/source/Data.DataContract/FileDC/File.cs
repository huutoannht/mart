using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.FileDC
{
    [Serializable]
    [DataContract]
    public class File : BaseDC
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string PhysicName { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public Guid CreatedBy { get; set; }
    }
}
