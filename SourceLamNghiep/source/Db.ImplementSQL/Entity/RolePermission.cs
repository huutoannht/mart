using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }
        public short PageId { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public bool CanExportExcel { get; set; }
        public bool CanImportExcel { get; set; }
    }
}
