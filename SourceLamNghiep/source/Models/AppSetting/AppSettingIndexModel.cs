using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.AppSetting
{
    public class AppSettingIndexModel : BaseIndexModel<AppSettingModel>
    {
        [Display(Name = "SettingType", ResourceType = typeof(Share.Messages.BeScreens.SiteSetting.SiteSetting))]
        public AppSettingType? SettingType { get; set; }
    }
}