using System;
using System.Runtime.Serialization;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class ApiImageUploadModel
    {
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// base64 string
        /// </summary>
        [DataMember]
        public string Data { get; set; }
    }
}
