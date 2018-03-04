using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESSPortal.Startup))]
namespace ESSPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
