using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Backend.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessageResourceName = "OldPasswordRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [Display(Name = "OldPassword", ResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [StringLength(100)]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [Display(Name = "NewPassword", ResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [StringLength(100)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceName = "ConfirmNewPasswordRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        [Compare("NewPassword", ErrorMessageResourceName = "PasswordDoesNotMatch", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Profile.Profile))]
        public string ConfirmNewPassword { get; set; }
    }
}