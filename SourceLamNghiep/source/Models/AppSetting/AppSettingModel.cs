using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.AppSetting
{
    [Serializable]
    public class AppSettingModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "SettingTypeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        [Display(Name = "SettingType", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        public AppSettingType SettingType { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        [Display(Name = "Name", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        [StringLength(500)]
        public string Name { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        public string Value { get; set; }

        //ref
        [Display(Name = "ImagePath", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        public string ImagePath { get; set; }
        public string OldImagePath { get; set; }

        public string Name2 { get; set; }

        [Display(Name = "ImagePath", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        public string ImagePath2 { get; set; }
        public string OldImagePath2 { get; set; }

        public Guid Id3 { get; set; }
        public string Name3 { get; set; }

        public string ImagePath3 { get; set; }
        public string OldImagePath3 { get; set; }

        public string ControllerReturn { get; set; }
        public string ActionReturn { get; set; }

        public string ControllerSave { get; set; }
        public string ActionSave { get; set; }

    }
}