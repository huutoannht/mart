using System;
using System.Runtime.Serialization;
using ML.Common;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid Id2 { get; set; }

        [DataMember]
        public bool IsNew
        {
            get { return Id == Guid.Empty; }
        }

        public void InitId()
        {
            Id = CombGuid.New;
        }
    }
}