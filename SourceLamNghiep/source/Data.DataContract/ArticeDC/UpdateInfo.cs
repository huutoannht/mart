using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.ArticeDC
{
    [DataContract]
    public class UpdateInfo
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ArticeStatus? Status { get; set; }
    }
}
