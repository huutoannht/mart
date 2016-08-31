using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.ProductDC
{
    [DataContract]
    [Serializable]
    public class ProductImageUpdateInfo : BaseDC
    {
        [DataMember]
        public bool? Visible { get; set; }

        [DataMember]
        public bool? Represent { get; set; }
    }
}
