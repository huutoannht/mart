﻿using System;
using System.Runtime.Serialization;

namespace Data.DataContract.CustomerDC
{
	[Serializable]
	[DataContract]
	public class CheckLoginRequest
	{
		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Password { get; set; }
	}
}
