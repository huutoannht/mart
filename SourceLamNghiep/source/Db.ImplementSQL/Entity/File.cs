using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.ImplementSQL.Entity
{
    [Serializable]
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string PhysicName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        //ref

        [ForeignKey("CreatedBy")]
        public BeUser BeUser { get ; set; }
    }
}
