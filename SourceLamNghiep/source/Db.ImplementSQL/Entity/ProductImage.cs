using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class ProductImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Visible { get; set; }
        public bool Represent { get; set; }

        //Ref

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
