using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Data.DataContract;
using Share.Helper.Cache;
using Share.Helper.Models;
using StructureMap;

namespace Models.Product
{
    public class ProductModel : BaseModel
    {
        public ProductModel()
        {
            ProductImages = new List<ProductImageModel>();
        }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public string Name { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public decimal Price { get; set; }

        [Display(Name = "Cost", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public decimal Cost { get; set; }

        [Display(Name = "Vat", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        [Range(0, 100)]
        public decimal Vat { get; set; }

        [Required(ErrorMessageResourceName = "CategoryRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessageResourceName = "ProductCodeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        [Display(Name = "ProductCode", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public string ProductCode { get; set; }

        public ProductStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [Required(ErrorMessageResourceName = "UOMRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        [Display(Name = "UOM", ResourceType = typeof(Share.Messages.BeScreens.ProductRes.ProductRes))]
        public Guid? UnitId { get; set; }

        //ref


        public Guid ParentCategoryId { get; set; }

        public List<ProductImageModel> ProductImages { get; set; }

        public string CategoryName()
        {
            var cache = ObjectFactory.GetInstance<ICacheHelper>();
            var cate = cache.GetCategories().FirstOrDefault(i => i.Id == CategoryId);
            return cate == null ? string.Empty : cate.Name;
        }

        public bool IsShopOptionsInViewGroupItem { get; set; }

        public string TextSearch { get; set; }

        public string VideosJsonString { get; set; }

    }
}