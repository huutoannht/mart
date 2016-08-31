using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class CustomerVisit : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public DateTime DateVisit { get; set; }
        public Guid BeUserId { get; set; }
        public string Promise { get; set; }
        public YesNo Done { get; set; }
    }
}
