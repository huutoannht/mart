using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.ArticeDC
{
    [Serializable]
    [DataContract]
    public class Artice : BaseDC
    {
        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public ArticeStatus Status { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Content { get; set; }
       
    }
}
