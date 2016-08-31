using System;
using System.Runtime.Serialization;

namespace Share.Helper.Models
{
    [Serializable]
    [DataContract]
    public class ReferenceModel
    {
	    public ReferenceModel()
	    {
	    }

	    public ReferenceModel(Guid id, string name) : this()
	    {
		    Id = id;
		    Name = name;
	    }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
		public string IdAsString { get { return Id.ToString(); } }
    }
}