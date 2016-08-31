using System;
using System.Runtime.Serialization;
using Data.DataContract;

namespace Data.DataContract
{
	[Serializable]
	[DataContract]
	public class BaseSaveRequest<T> where T : BaseDC
	{
		[DataMember]
		public T Entity { get; set; }
	}
}
