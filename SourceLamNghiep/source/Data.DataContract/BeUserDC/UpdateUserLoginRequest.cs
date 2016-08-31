using System;
using System.Runtime.Serialization;

namespace Data.DataContract.BeUserDC
{
	[Serializable]
	[DataContract]
	public class UpdateUserLoginRequest
	{
		[DataMember]
		public Guid UserId { get; set; }
	}
}
