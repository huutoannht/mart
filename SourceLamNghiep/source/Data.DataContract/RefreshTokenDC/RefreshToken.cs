using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContract.RefreshTokenDC
{
    [Serializable]
    [DataContract]
    public class RefreshToken
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public DateTime IssuedUtc { get; set; }

        [DataMember]
        public DateTime ExpiresUtc { get; set; }

        [DataMember]
        public string ProtectedTicket { get; set; }
    }
}
