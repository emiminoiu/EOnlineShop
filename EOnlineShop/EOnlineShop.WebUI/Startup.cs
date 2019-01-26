using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EOnlineShop.WebUI.Startup))]
namespace EOnlineShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
