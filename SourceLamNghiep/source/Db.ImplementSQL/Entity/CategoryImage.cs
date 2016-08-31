using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class CategoryImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Visible { get; set; }

        //Ref

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
