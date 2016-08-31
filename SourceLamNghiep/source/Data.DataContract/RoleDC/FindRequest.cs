using System.Runtime.Serialization;

namespace Data.DataContract.RoleDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}
