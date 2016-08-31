using System;
using System.Runtime.Serialization;


namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class Money
    {
        public Money()
        {
            CurrencySymbol = string.Empty;
            CurrencyCode = string.Empty;
        }

        [DataMember]
        public decimal Value { get; set; }

        [DataMember]
        public string CurrencySymbol { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }

        [DataMember]
        public string ValueFormated { get; set; }
    }
}
