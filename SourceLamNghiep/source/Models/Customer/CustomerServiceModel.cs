using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.Customer
{
    public class CustomerServiceModel : BaseModel
    {
        public Guid CustomerId { get; set; }

        [Required(ErrorMessageResourceName = "DateCustomerServiceRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "Date", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public DateTime DateCS { get; set; }

        [Required(ErrorMessageResourceName = "StaffRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "Staff", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid BeUserId { get; set; }

        [Display(Name = "Promise", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string Promise { get; set; }

        [Display(Name = "Done", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public YesNo Done { get; set; }
    }
}
