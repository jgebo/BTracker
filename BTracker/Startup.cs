using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTracker.Startup))]
namespace BTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
