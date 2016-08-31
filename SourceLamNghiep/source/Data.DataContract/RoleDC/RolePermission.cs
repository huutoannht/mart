using System;
using System.Runtime.Serialization;

namespace Data.DataContract.RoleDC
{
    [Serializable]
    [DataContract]
    public class RolePermission : BaseDC
    {
        [DataMember]
        public Guid RoleId { get; set; }

        [DataMember]
        public short PageId { get; set; }

        [DataMember]
        public bool CanView { get; set; }

        [DataMember]
        public bool CanAdd { get; set; }

        [DataMember]
        public bool CanUpdate { get; set; }

        [DataMember]
        public bool CanDelete { get; set; }

        [DataMember]
        public bool CanPrint { get; set; }

        [DataMember]
        public bool CanExportExcel { get; set; }

        [DataMember]
        public bool CanImportExcel { get; set; }
    }
}
