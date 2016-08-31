using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract.ProductDC
{
    [DataContract]
    [Serializable]
    public class Product : BaseDC
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public decimal Vat { get; set; }

        [DataMember]
        public Guid CategoryId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public ProductStatus Status { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public Guid? UnitId { get; set; }

        //Ref

        [DataMember]
        public List<ProductImage> ProductImages { get; set; }
    }
}
