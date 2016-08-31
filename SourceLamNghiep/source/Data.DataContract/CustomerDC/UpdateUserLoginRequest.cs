using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
	[Serializable]
	[DataContract]
	public class UpdateUserLoginRequest
	{
		[DataMember]
		public Guid UserId { get; set; }
	}
}
