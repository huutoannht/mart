using System;
using System.Runtime.Serialization;
using Data.DataContract;

namespace Data.DataContract.AppSettingDC
{
    [DataContract]
    [Serializable]
    public class AppSetting : BaseDC
    {      
        [DataMember]
        public AppSettingType SettingType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int Priority { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        [DataMember]
        public bool LoginRestricted { get; set; }
    }
}
