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
    public class ExchangeRateConverterModel
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Target { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
