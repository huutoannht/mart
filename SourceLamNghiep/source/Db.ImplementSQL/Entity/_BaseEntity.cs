using System;
using System.ComponentModel.DataAnnotations;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

    }
}
