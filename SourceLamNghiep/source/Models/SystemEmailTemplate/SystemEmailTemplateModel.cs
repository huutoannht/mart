using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.SystemEmailTemplate
{
    [Serializable]
    public class SystemEmailTemplateModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        [Display(Name = "Type", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public SystemEmailTemplateType Type { get; set; }

        [Required(ErrorMessageResourceName = "LanguageRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        [Display(Name = "Language", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public string Language { get; set; }

        [Required(ErrorMessageResourceName = "SubjectRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        [Display(Name = "Subject", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public string Subject { get; set; }

        [Display(Name = "Content", ResourceType = typeof(Share.Messages.BeScreens.SystemEmailTemplate.SystemEmailTemplate))]
        public string Content { get; set; }
    }
}