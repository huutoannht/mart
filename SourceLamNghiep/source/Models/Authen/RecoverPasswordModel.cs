using System.ComponentModel.DataAnnotations;
using Share.Messages.BeScreens.Login;

namespace Models.Authen
{
    public class RecoverPasswordModel
    {
        [Required(ErrorMessageResourceName = "EmailRequire", ErrorMessageResourceType = typeof(Login), ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Login))]
        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid", ErrorMessageResourceType = typeof(Login), ErrorMessage = null)]
        public string Email { get; set; }
    }
}