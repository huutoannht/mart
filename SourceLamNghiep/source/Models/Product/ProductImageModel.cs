using System;
using Share.Helper.Models;

namespace Models.Product
{
    public class ProductImageModel : BaseModel
    {
        public string ImagePath { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Visible { get; set; }
        public bool Represent { get; set; }
    }
}