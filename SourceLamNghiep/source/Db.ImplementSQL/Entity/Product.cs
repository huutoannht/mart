using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Vat { get; set; }

        public Guid CategoryId { get; set; }

        public string ProductCode { get; set; }

        public ProductStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public Guid? UnitId { get; set; }

        //ref
        public List<ProductImage> ProductImages { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("UnitId")]
        public Category Unit { get; set; }
    }
}
