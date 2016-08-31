using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.CategoryDC
{
    [Serializable]
    [DataContract]
    public class CategoryImage : BaseDC
    {
        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public Guid CategoryId { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool Visible { get; set; }
    }
}
