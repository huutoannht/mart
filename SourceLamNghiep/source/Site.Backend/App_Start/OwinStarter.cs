using Microsoft.AspNet.SignalR;
using Site.Backend;
using Site.Backend.Library.DI;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OwinStarter))]
namespace Site.Backend
{
    public class OwinStarter
    {
        public void Configuration(IAppBuilder app)
        {
            IoCConfiguration.Configuration(app);

            app.MapSignalR();
        }
    }
}