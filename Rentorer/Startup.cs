using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rentorer.Startup))]
namespace Rentorer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
