using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Data.DataContract.ProductDC
{
    [DataContract]
    [Serializable]
    public class ProductImage : BaseDC
    {
        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool Visible { get; set; }

        [DataMember]
        public bool Represent { get; set; }
    }
}
