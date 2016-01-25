using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmilioMarket.Startup))]
namespace EmilioMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
