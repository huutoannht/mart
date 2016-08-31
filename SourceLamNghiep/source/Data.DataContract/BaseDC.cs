using System;
using System.Runtime.Serialization;
using ML.Common;
using Data.DataContract;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class BaseDC : BaseDCNoId
    {
        [DataMember]
        public Guid Id { get; set; }

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
