using System.ComponentModel.DataAnnotations;
using Data.DataContract;

namespace Models.SystemEmailTemplate
{
    public class SystemEmailTemplateIndexModel
    {
        [Display(Name = "Type", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public SystemEmailTemplateType? Type { get; set; }

        [Required(ErrorMessageResourceName = "LanguageRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        [Display(Name = "Language", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public string Language { get; set; }

        public SystemEmailTemplateModel SystemEmailTemplate { get; set; }
    }
}