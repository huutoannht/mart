using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class DataLog : BaseEntity
    {
        public TableLog Table { get; set; }
        public ActionLog Action { get; set; }
        public Guid BeUserId { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime LogDate { get; set; }
        public Guid ItemId { get; set; }

        //ref

        [ForeignKey("BeUserId")]
        public BeUser BeUser { get; set; }
    }
}
