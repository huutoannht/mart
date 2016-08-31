using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.DataLog
{
    public class DataLogModel : BaseModel
    {
        public TableLog Table { get; set; }
        public ActionLog Action { get; set; }
        public Guid BeUserId { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime LogDate { get; set; }
        public Guid ItemId { get; set; }
    }
}
