using System;
using System.Runtime.Serialization;
using Data.DataContract;

namespace Data.DataContract.SystemEmailTemplateDC
{
    [DataContract]
    [Serializable]
    public class SystemEmailTemplate : BaseDC
    {    
        [DataMember]
        public SystemEmailTemplateType Type { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}
