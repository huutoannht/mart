using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Backend.Models
{
    public class ComposeMailModel
    {
        [Display(Name = "Emails", ResourceType = typeof(Share.Messages.BeScreens.GuiMail.GuiMail))]
        public string Emails { get; set; }

        [Required(ErrorMessageResourceName = "SubjectRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.GuiMail.GuiMail))]
        [Display(Name = "Subject", ResourceType = typeof(Share.Messages.BeScreens.GuiMail.GuiMail))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "ContentRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.GuiMail.GuiMail))]
        [Display(Name = "Content", ResourceType = typeof(Share.Messages.BeScreens.GuiMail.GuiMail))]
        public string Content { get; set; }

        public bool SendToAllRegisteredCustomer { get; set; }
    }
}