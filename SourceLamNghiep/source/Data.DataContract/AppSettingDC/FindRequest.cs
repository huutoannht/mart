using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract.AppSettingDC
{
    [Serializable]
    [DataContract]
    public class FindRequest : BaseFindRequest
    {        
        [DataMember]
        public AppSettingType? SettingType { get; set; }

        [DataMember]
        public List<AppSettingType> ExcludeSettingTypes { get; set; }
    }
}
