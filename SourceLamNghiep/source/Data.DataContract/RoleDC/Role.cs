using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Data.DataContract;

namespace Data.DataContract.RoleDC
{
    [DataContract]
    [Serializable]
    public class Role : BaseDC
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<RolePermission> Permissions { get; set; }
    }
}
