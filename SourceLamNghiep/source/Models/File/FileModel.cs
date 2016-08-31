using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.Helper.Models;

namespace Models.File
{
    public class FileModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Files.Files))]
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.Files.Files))]
        public string Name { get; set; }

        public string PhysicName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        //ref 
        public string OldPhysicName { get; set; }
    }
}
