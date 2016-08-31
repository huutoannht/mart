using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class CustomerImage : BaseDC
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}
