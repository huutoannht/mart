using System.Runtime.Serialization;

namespace Data.DataContract.CategoryDC
{
    [DataContract]
    public class FindRequest : BaseFindRequest
    {
        [DataMember]
        public CategoryType? CategoryType { get; set; }

        [DataMember]
        public string TextSearch { get; set; }
    }
}
