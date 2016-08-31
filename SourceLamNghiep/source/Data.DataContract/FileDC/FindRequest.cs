using System.Runtime.Serialization;

namespace Data.DataContract.FileDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string TextSearch { get; set; }
    }
}
