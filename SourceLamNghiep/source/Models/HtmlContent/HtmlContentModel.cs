using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;
using Share.Messages.BeScreens.MessageSetup;

namespace Models.HtmlContent
{
    public class HtmlContentModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "LanguageCodeRequired", ErrorMessageResourceType = typeof(MessageSetup))]
        [Display(Name = "LanguageCode", ResourceType = typeof(MessageSetup))]
        public string LanguageCode { get; set; }

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(MessageSetup))]
        [Display(Name = "Type", ResourceType = typeof(MessageSetup))]
        public short Type { get; set; }

        [Display(Name = "Content", ResourceType = typeof(MessageSetup))]
        public string Content { get; set; }

        [Required(ErrorMessageResourceName = "HtmlContentDisplayTypeRequired", ErrorMessageResourceType = typeof(MessageSetup))]
        [Display(Name = "HtmlContentDisplayType", ResourceType = typeof(MessageSetup))]
        public HtmlContentDisplayType HtmlContentDisplayType { get; set; }

        [Display(Name = "Header", ResourceType = typeof(MessageSetup))]
        public string Header { get; set; }
    }
}