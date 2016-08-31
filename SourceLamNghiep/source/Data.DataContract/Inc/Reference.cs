using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class Reference
    {
	    public Reference()
	    {
			ExtendData = new Dictionary<string, object>();
	    }

		public Reference(Guid id, string name = "") : this()
		{
			Id = id;
			Name = name;
		}

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

		[DataMember]
	    public Dictionary<string, object> ExtendData { get; set; }

		public string IdAsString { get { return Id.ToString(); } }
    }
}
