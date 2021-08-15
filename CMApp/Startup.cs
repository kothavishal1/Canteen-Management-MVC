using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMApp.Startup))]
namespace CMApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
