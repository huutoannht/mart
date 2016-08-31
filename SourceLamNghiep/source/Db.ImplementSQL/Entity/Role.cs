using System;
using System.Collections.Generic;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<RolePermission> Permissions { get; set; }
    }
}
