using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.Product
{
    public class ProductIndexModel : BaseIndexModel<ProductModel>
    {
        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
        public string TextSearch { get; set; }
    }
}