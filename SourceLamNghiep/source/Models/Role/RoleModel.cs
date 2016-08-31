using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Share.Helper.Models;

namespace Models.Role
{
    public class RoleModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Role.Role))]
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.Role.Role))]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Share.Messages.BeScreens.Role.Role))]
        public string Description { get; set; }

        public List<RolePermissionModel> Permissions { get; set; }
    }
}