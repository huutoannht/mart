using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class CustomerImage : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
