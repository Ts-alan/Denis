using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sciencecom.Startup))]
namespace Sciencecom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
