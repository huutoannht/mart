using System;
using System.Collections.Generic;
using Data.DataContract;

namespace Site.Backend.Library.Helper
{
	public interface IAuthenHelper
	{
		bool IsAuthenticated { get; }

		bool SessionIsAlive { get; }

		void Logout();

        bool CanAccessArea(BeUserType beUserType);
	}

	public class AuthenException : Exception
	{
		public AuthenException()
		{
		}

		public AuthenException(string message)
			: base(message)
		{
		}

		public AuthenException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}