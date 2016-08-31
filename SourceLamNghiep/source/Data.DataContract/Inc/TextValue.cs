using System;
using System.Runtime.Serialization;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class TextValue
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

}
