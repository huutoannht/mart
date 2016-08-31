using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Xml;
using StructureMap;

namespace Site.Backend.App_Start
{
    public class MvcSiteMapProviderConfig
    {
        public static void Register(IContainer container)
        {
            // Setup global sitemap loader
            MvcSiteMapProvider.SiteMaps.Loader = container.GetInstance<ISiteMapLoader>();

            // Check all configured .sitemap files to ensure they follow the XSD for MvcSiteMapProvider
            var validator = container.GetInstance<ISiteMapXmlValidator>();
            validator.ValidateXml(HostingEnvironment.MapPath("~/Mvc.sitemap"));

            // Register the Sitemaps routes for search engines
            XmlSiteMapController.RegisterRoutes(RouteTable.Routes);
        }
    }
}