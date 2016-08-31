using System;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.Customer
{
	[Serializable]
    public class CustomerIndexModel : BaseIndexModel<CustomerModel>
	{
        public CustomerType? CustomerType { get; set; }

        [Display(Name = "ClinicId", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string ClinicId { get; set; }

        [Display(Name = "ClinicName", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string ClinicName { get; set; }

        [Display(Name = "DentistName", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string DentistName { get; set; }

        [Display(Name = "ClinicPhone", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string ClinicPhone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string Address { get; set; }

        [Display(Name = "City", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public short? City { get; set; }

        [Display(Name = "District", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public short? District { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string Email { get; set; }

        [Display(Name = "InterestingDevice", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? InterestingDevice { get; set; }

        [Display(Name = "Staff", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? AssignTo { get; set; }

        [Display(Name = "UsingRC", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? UsingRC { get; set; }

        [Display(Name = "VisitFrom", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public DateTime? VisitFrom { get; set; }

        [Display(Name = "VisitTo", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public DateTime? VisitTo { get; set; }

        public bool IsExport { get; set; }
	}
}