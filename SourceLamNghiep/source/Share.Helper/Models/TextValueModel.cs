using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class TextValueModel
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public List<TextValueModel> Childs { get; set; }
    }
}