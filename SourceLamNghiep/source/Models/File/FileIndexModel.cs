using System.ComponentModel.DataAnnotations;
using Share.Helper.Models;

namespace Models.File
{
    public class FileIndexModel : BaseIndexModel<FileModel>
    {
        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
        public string TextSearch { get; set; }
    }
}
