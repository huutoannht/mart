using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class BaseFindResponse<T> where T : class
    {
        public BaseFindResponse()
        {
            Results = new List<T>();
        }

        [DataMember]
        public List<T> Results { get; set; }

        /// <summary>
        /// only valid when pageoption is valid
        /// </summary>
        [DataMember]
        public int TotalRecords { get; set; }
    }
}
