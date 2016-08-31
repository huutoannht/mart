using System;
using System.Runtime.Serialization;

namespace Data.DataContract.BeUserDC
{
    [Serializable]
    [DataContract]
    public class BeUser : BaseDC
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string HashPassword { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string TimeZoneId { get; set; }

        [DataMember]
        public DateTime? LastLoginDate { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public BeUserType Type { get; set; }

        [DataMember]
        public Guid? RoleId { get; set; }

        [DataMember]
        public string PreferLanguage { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public Gender? Gender { get; set; }

        [DataMember]
        public DateTime? BirthDay { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public Guid? PositionId { get; set; }

        [DataMember]
        public BeUserStatus Status { get; set; }
    }
}
