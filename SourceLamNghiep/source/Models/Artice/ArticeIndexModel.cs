using System.ComponentModel.DataAnnotations;
using Share.Helper.Models;

namespace Models.Artice
{
    public class ArticeIndexModel : BaseIndexModel<ArticeModel>
    {
        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
        public string TextSearch { get; set; }
    }
}
