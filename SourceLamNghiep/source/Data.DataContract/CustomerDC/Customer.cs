using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class Customer : BaseDC
    {
        [DataMember]
        public string ClinicEmail { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public CustomerType CustomerType { get; set; }

        [DataMember]
        public string ClinicId { get; set; }

        [DataMember]
        public string ClinicName { get; set; }

        [DataMember]
        public short City { get; set; }

        [DataMember]
        public short District { get; set; }

        [DataMember]
        public string ClinicPhone { get; set; }

        [DataMember]
        public string Website { get; set; }

        [DataMember]
        public Guid? NumberOfDentist { get; set; }

        [DataMember]
        public Guid? NumberOfStaff { get; set; }

        [DataMember]
        public Guid? NumberOfChair { get; set; }

        [DataMember]
        public Guid? UsingRC { get; set; }

        [DataMember]
        public string UsingDevices { get; set; }

        [DataMember]
        public Guid CreatedBy { get; set; }

        [DataMember]
        public double? Lat { get; set; }

        [DataMember]
        public double? Lng { get; set; }

        #region Dentist

        [DataMember]
        public string DentistName { get; set; }

        [DataMember]
        public Gender? Gender { get; set; }

        [DataMember]
        public Guid? Age { get; set; }

        [DataMember]
        public string DentistPhone { get; set; }

        [DataMember]
        public string DentistEmail { get; set; }

        [DataMember]
        public Guid? Specialization { get; set; }

        [DataMember]
        public MaritalStatus? MaritalStatus { get; set; }

        [DataMember]
        public YesNo? JoinSeminar { get; set; }

        [DataMember]
        public DateTime? JoinedDate { get; set; }

        [DataMember]
        public string InterestingDevice { get; set; }

        [DataMember]
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

        [DataMember]
        public string AssignTo { get; set; }

        [DataMember]
        public string Remark { get; set; }

        //ref

        [DataMember]
        public List<CustomerImage> Images { get; set; }

        public List<CustomerVisit> Visits { get; set; }
    }
}
