using System;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class BeUser : BaseEntity
    {
        public string Code { get; set; }

        public string HashPassword { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string TimeZoneId { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ImagePath { get; set; }

        public BeUserType Type { get; set; }

        public Guid? RoleId { get; set; }

        public string PreferLanguage { get; set; }

        public string Phone { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }

        public Guid? PositionId { get; set; }

        public BeUserStatus Status { get; set; }

        //ref

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PositionId")]
        public Category Position { get; set; }
    }
}
