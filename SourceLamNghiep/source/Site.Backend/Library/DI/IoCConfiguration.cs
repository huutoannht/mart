using Models;
using Site.Backend.App_Start;
using Owin;
using StructureMap;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace Site.Backend.Library.DI
{
	public class IoCConfiguration
	{
		public static void Configuration(IAppBuilder app)
		{
            var container = Initialize(initialize =>
			                               {
                                               initialize.AddRegistry<SiteRegistry>();
                                               initialize.AddRegistry<MvcSiteMapProviderRegistry>();
                                               Share.DI.Configuration.Instance.Configure(initialize);
                                               Share.Helper.Configuration.Instance.Configure(initialize);
			                               });

			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

            MvcSiteMapProviderConfig.Register(container);

			GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);

			AutoMapperConfiguration.Instance.Configure();
            AutoMapperCommonConfiguration.Instance.Configure();
		}

		public static IContainer Initialize(Action<IInitializationExpression> extendInitialize = null)
		{
			ObjectFactory.Initialize(initialize =>
			{
				initialize.Scan(scan => scan.WithDefaultConventions());

				if (extendInitialize != null)
				{
					extendInitialize(initialize);
				}

			});

			return ObjectFactory.Container;
		}
	}
}