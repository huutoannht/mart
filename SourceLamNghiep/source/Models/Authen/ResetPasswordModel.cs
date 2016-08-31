using System.ComponentModel.DataAnnotations;
using Share.Messages.BeScreens.Login;

namespace Models.Authen
{
    public class ResetPasswordModel
    {
        [Required]
        public string KeyRef { get; set; }

        [Required(ErrorMessageResourceName = "NewPasswordRequired", ErrorMessageResourceType = typeof(Login))]
        [Display(Name = "NewPassword", ResourceType = typeof(Login))]
        [DataType(DataType.Password)]        
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceName = "PasswordNotMatch", ErrorMessageResourceType = typeof(Login))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Login))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}