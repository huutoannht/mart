using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public CustomerType? CustomerType { get; set; }

        [DataMember]
        public string ClinicId { get; set; }

        [DataMember]
        public string ClinicName { get; set; }

        [DataMember]
        public string DentistName { get; set; }

        [DataMember]
        public string ClinicPhone { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public short? City { get; set; }

        [DataMember]
        public short? District { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public Guid? InterestingDevice { get; set; }

        [DataMember]
        public Guid? AssignTo { get; set; }

        [DataMember]
        public Guid? UsingRC { get; set; }

        [DataMember]
        public DateTime? VisitFrom { get; set; }

        [DataMember]
        public DateTime? VisitTo { get; set; }
    }
}
