using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Backend.Models
{
    public class SettingModel
    {
        public string CompanyName { get; set; }
        public string Tax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }

        public string ImagePath { get; set; }
        public string OldImagePath { get; set; }

        public string ImagePath2 { get; set; }
        public string OldImagePath2 { get; set; }

        public string ImagePath3 { get; set; }
        public string OldImagePath3 { get; set; }
    }
}