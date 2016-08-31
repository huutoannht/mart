using System.ComponentModel.DataAnnotations;
using Share.Helper.Models;

namespace Models.Role
{
    public class RoleIndexModel : BaseIndexModel<RoleModel>
    {
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.Role.Role))]
        public string Name { get; set; }
    }
}