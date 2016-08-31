using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Data.DataContract;
using ML.Common;
using Share.Helper.ModelBinder;
using Site.Backend.Library.Session;
using Share.Helper;
using ML.Common.Log;
using StructureMap;

namespace Site.Backend
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            LogManager.Configure(@Server.MapPath("~/Config/log4net.config"));

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitModelBinders();
        }

        protected void Application_BeginRequest()
        {
            var sessionHelper = ObjectFactory.GetInstance<ISessionHelper>();
            var languageCode = Request.IsAuthenticated
                ? SiteUtils.GetCurrentBeUserLanguageCode(new HttpRequestWrapper(Request), sessionHelper.CurrentUserId)
                : SiteUtils.GetCurrentBeUserLanguageCode(new HttpRequestWrapper(Request), Guid.Empty);

            var culture = new CultureInfo(languageCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            
        }

        public void Session_OnStart()
        {
            if (Request.IsAuthenticated && Session[SessionKey.CurrentUserId.ToString()] == null)
            {
                var ck = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (ck != null)
                {
                    var oldTicket = FormsAuthentication.Decrypt(ck.Value);
                    var sessionHelper = ObjectFactory.GetInstance<ISessionHelper>();
                    sessionHelper.CurrentUserId = oldTicket.Name.ToGuid();
                }
            }
        }

        private void InitModelBinders()
        {
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());

            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
        }
    }
}