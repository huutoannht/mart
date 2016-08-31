using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class EnumModel
    {
        [DataMember]
        public short Value { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NameLocal { get; set; }

        [DataMember]
        public object Data { get; set; }

        [DataMember]
        public short ParentId { get; set; }
    }
}
