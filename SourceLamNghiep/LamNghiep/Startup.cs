using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LamNghiep.Startup))]
namespace LamNghiep
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
