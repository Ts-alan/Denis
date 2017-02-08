using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OddBasyBY.Startup))]
namespace OddBasyBY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
