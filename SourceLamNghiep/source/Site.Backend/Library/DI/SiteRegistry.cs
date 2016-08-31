using Site.Backend.Library.Helper;
using Site.Backend.Library.Helper.Implements;
using Site.Backend.Library.Session;
using ML.Common;
using ML.Utils.Email;
using StructureMap.Configuration.DSL;

namespace Site.Backend.Library.DI
{
	internal class SiteRegistry : Registry
	{
		public SiteRegistry()
		{
			For<ISessionHelper>().Use<SessionHelper>();

			For<IAuthenHelper>().Use<AuthenHelper>();
		}
	}
}