using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.Category
{
    public class CategoryIndexModel : BaseIndexModel<CategoryModel>
    {
        public CategoryType CategoryType { get; set; }

        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
        public string TextSearch { get; set; }
    }
}
