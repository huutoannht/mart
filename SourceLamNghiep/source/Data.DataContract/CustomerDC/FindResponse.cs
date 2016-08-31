using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
    [Serializable]
    [DataContract]
    public class FindResponse : BaseFindResponse<Customer>
    {

    }
}
