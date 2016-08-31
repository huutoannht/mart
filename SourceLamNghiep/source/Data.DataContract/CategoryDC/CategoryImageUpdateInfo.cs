using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.CategoryDC
{
    [DataContract]
    [Serializable]
    public class CategoryImageUpdateInfo : BaseDC
    {
        [DataMember]
        public bool? Visible { get; set; }
    }
}
