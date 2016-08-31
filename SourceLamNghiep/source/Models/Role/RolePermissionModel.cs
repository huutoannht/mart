using System;
using Share.Helper.Models;

namespace Models.Role
{
    public class RolePermissionModel : BaseModel
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

        //Ref
        public string PageName { get; set; }
    }
}