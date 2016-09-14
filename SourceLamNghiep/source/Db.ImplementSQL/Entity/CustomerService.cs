using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class CustomerService : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public DateTime DateCS { get; set; }
        public Guid BeUserId { get; set; }
        public string Promise { get; set; }
        public YesNo Done { get; set; }
    }
}
