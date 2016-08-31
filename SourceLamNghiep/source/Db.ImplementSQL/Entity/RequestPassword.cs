using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class RequestPassword : BaseEntity
    {
        public string KeyRef { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
    }
}
