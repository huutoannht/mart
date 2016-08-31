using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share.Helper.Models;

namespace Models.Customer
{
    public class CustomerImageModel : BaseModel 
    {
        public Guid CustomerId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
