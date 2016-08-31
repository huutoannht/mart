using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.BeUser
{
    [Serializable]
    public class BeUserModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "CodeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [Display(Name = "Code", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "TypeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [Display(Name = "Type", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public BeUserType Type { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public Guid? RoleId { get; set; }
       
        public string HashPassword { get; set; }

        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator), ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [StringLength(100)]
        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FullNameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [Display(Name = "FullName", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [StringLength(500)]
        public string FullName { get; set; }

        [Display(Name = "TimeZone", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public string TimeZoneId { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Password { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public string ImagePath { get; set; }

        [Display(Name = "PreferLanguage", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public string PreferLanguage { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public Gender? Gender { get; set; }

        [Display(Name = "BirthDay", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public string Address { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public Guid? PositionId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Share.Messages.BeScreens.Administrator.Administrator))]
        public BeUserStatus Status { get; set; }

        //ref
        public string OldImagePath { get; set; }
    }
}