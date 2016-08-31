using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Models.Product;
using Share.Helper;
using Share.Helper.Models;
using Share.Messages;

namespace Models.Category
{
    public class CategoryModel : BaseModel
    {
        public CategoryModel()
        {
            Images = new List<CategoryImageModel>();
            Childs = new List<CategoryModel>();
        }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CategoryRes.CategoryRes))]
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.CategoryRes.CategoryRes))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "CodeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CategoryRes.CategoryRes))]
        [Display(Name = "Code", ResourceType = typeof(Share.Messages.BeScreens.CategoryRes.CategoryRes))]
        public string Code { get; set; }

        public Guid? ParentId { get; set; }

        public string ImagePath { get; set; }

        public int SortOrder { get; set; }

        public CategoryType CategoryType { get; set; }

        //Ref

        public string OldImagePath { get; set; }

        public string LevelTreeView { get; set; }

        public string ParentLevelTreeView { get; set; }

        public int Level { get; set; }

        public int LevelPadding { get; set; }

        public int OrderNumber { get; set; }

        public List<CategoryModel> Childs { get; set; }

        public List<ProductModel> Products { get; set; }

        public int ProductCount { get; set; }

        public List<CategoryImageModel> Images { get; set; }

        public string ImageFileNames { get; set; }
    }
}
