using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceControl.Startup))]
namespace ServiceControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
