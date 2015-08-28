using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuickStart.Startup))]
namespace QuickStart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
