using System;
using System.ComponentModel.DataAnnotations;
using Share.Messages.BeScreens.Login;

namespace Models.Authen
{
    [Serializable]
    public class AuthenViewModel
    {
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Login))]
        [Display(Name = "Email", ResourceType = typeof(Login))]
        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Login))]
        [Display(Name = "Password", ResourceType = typeof(Login))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Login))]
        public bool Remember { get; set; }

        public string ReturnUrl { get; set; }
    }
}