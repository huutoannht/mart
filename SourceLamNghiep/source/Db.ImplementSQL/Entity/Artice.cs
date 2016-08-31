using System;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class Artice : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }
        public ArticeStatus Status { get; set; }
    }
}
