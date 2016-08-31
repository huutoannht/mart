using System;
using System.Collections.Generic;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class Customer : BaseEntity
    {
        public string ClinicEmail { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public CustomerType CustomerType { get; set; }
        public string ClinicId { get; set; }
        public string ClinicName { get; set; }
        public short City { get; set; }
        public short District { get; set; }
        public string ClinicPhone { get; set; }
        public string Website { get; set; }
        public Guid? NumberOfDentist { get; set; }
        public Guid? NumberOfStaff { get; set; }
        public Guid? NumberOfChair { get; set; }
        public Guid? UsingRC { get; set; }
        public string UsingDevices { get; set; }
        public Guid CreatedBy { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        #region Dentist

        public string DentistName { get; set; }

        public Gender? Gender { get; set; }

        public Guid? Age { get; set; }

        public string DentistPhone { get; set; }

        public string DentistEmail { get; set; }

        public Guid? Specialization { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public YesNo? JoinSeminar { get; set; }

        public DateTime? JoinedDate { get; set; }

        public string InterestingDevice { get; set; }

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

        public string AssignTo { get; set; }

        public string Remark { get; set; }

        //ref

        public List<CustomerImage> Images { get; set; }

        public List<CustomerVisit> Visits { get; set; }
    }
}
