using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.Helper.Models;

namespace Models.Category
{
    public class CategoryImageModel : BaseModel
    {
        public string ImagePath { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Visible { get; set; }
    }
}
