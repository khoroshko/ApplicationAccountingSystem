using ApplicationAccountingSystemWeb.Modules;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApplicationAccountingSystemWeb.Startup))]
namespace ApplicationAccountingSystemWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
