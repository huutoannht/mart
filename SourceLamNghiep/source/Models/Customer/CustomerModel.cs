using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Models.DataLog;
using Share.Helper.Models;

namespace Models.Customer
{
    public class CustomerModel : BaseModel
    {
        public CustomerModel()
        {
            Visits = new List<CustomerVisitModel>();
            VisitIndex = new CustomerVisitIndexModel();
            CustomerServices = new List<CustomerServiceModel>();
            CustomerServiceIndex = new CustomerServiceIndexModel();
            DataLogIndex = new DataLogIndexModel();
        }

        //[Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(50)]
        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        public string ClinicEmail { get; set; }

        [Required(ErrorMessageResourceName = "ClinicIdRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "ClinicId", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(200)]
        public string ClinicId { get; set; }

        [Required(ErrorMessageResourceName = "ClinicNameRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "ClinicName", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(2000)]
        public string ClinicName { get; set; }

        [Display(Name = "City", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public short City { get; set; }

        [Display(Name = "District", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public short District { get; set; }

        [Display(Name = "ClinicPhone", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(50)]
        public string ClinicPhone { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(3000)]
        public string Address { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string Website { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessageResourceName = "CustomerTypeRequired", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "CustomerType", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public CustomerType CustomerType { get; set; }

        [Display(Name = "NumberOfDentist", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? NumberOfDentist { get; set; }

        [Display(Name = "NumberOfStaff", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? NumberOfStaff { get; set; }

        [Display(Name = "NumberOfChair", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? NumberOfChair { get; set; }

        [Display(Name = "UsingRC", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? UsingRC { get; set; }

        [Display(Name = "UsingDevice", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string UsingDevices { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid CreatedBy { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        #region Dentist

        [Display(Name = "DentistName", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(1000)]
        public string DentistName { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Gender? Gender { get; set; }

        [Display(Name = "Age", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? Age { get; set; }

        [Display(Name = "DentistPhone", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(50)]
        public string DentistPhone { get; set; }

        [EmailAddress(ErrorMessageResourceName = "EmailIsInvalid", ErrorMessageResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes), ErrorMessage = null)]
        [Display(Name = "DentistEmail", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        [StringLength(200)]
        public string DentistEmail { get; set; }

        [Display(Name = "Specialization", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? Specialization { get; set; }

        [Display(Name = "MaritalStatus", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public MaritalStatus? MaritalStatus { get; set; }

        [Display(Name = "JoinSeminar", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public YesNo? JoinSeminar { get; set; }

        [Display(Name = "JoinedDate", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public DateTime? JoinedDate { get; set; }

        [Display(Name = "InterestingDevice", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string InterestingDevice { get; set; }

        [Display(Name = "LeadTime", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public Guid? LeadTime { get; set; }

        #endregion

        #region working hours

        public bool SundayWork { get; set; }
        public DateTime? SundayStart { get; set; }
        public DateTime? SundayEnd { get; set; }
        public bool MondayWork { get; set; }
        public DateTime? MondayStart { get; set; }
        public DateTime? MondayEnd { get; set; }
        public bool TuesdayWork { get; set; }
        public DateTime? TuesdayStart { get; set; }
        public DateTime? TuesdayEnd { get; set; }
        public bool WednesdayWork { get; set; }
        public DateTime? WednesdayStart { get; set; }
        public DateTime? WednesdayEnd { get; set; }
        public bool ThursdayWork { get; set; }
        public DateTime? ThursdayStart { get; set; }
        public DateTime? ThursdayEnd { get; set; }
        public bool FridayWork { get; set; }
        public DateTime? FridayStart { get; set; }
        public DateTime? FridayEnd { get; set; }
        public bool SaturdayWork { get; set; }
        public DateTime? SaturdayStart { get; set; }
        public DateTime? SaturdayEnd { get; set; }

        #endregion

        [Display(Name = "AssignTo", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string AssignTo { get; set; }

        [Display(Name = "Remark", ResourceType = typeof(Share.Messages.BeScreens.CustomerRes.CustomerRes))]
        public string Remark { get; set; }

        //ref

        public List<CustomerImageModel> Images { get; set; }

        public List<CustomerVisitModel> Visits { get; set; }

        public List<CustomerServiceModel> CustomerServices { get; set; }

        public CustomerVisitIndexModel VisitIndex { get; set; }

        public CustomerServiceIndexModel CustomerServiceIndex { get; set; }

        public DataLogIndexModel DataLogIndex { get; set; }

        public string VisitJsonString { get; set; }

        public string CustomerServiceJsonString { get; set; }

        public bool View { get; set; }

        #region working hours

        public string SundayStartStr { get; set; }
        public string SundayEndStr { get; set; }
        public string MondayStartStr { get; set; }
        public string MondayEndStr { get; set; }
        public string TuesdayStartStr { get; set; }
        public string TuesdayEndStr { get; set; }
        public string WednesdayStartStr { get; set; }
        public string WednesdayEndStr { get; set; }
        public string ThursdayStartStr { get; set; }
        public string ThursdayEndStr { get; set; }
        public string FridayStartStr { get; set; }
        public string FridayEndStr { get; set; }
        public string SaturdayStartStr { get; set; }
        public string SaturdayEndStr { get; set; }

        #endregion
    }
}