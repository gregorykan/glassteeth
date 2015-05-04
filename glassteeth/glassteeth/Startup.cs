using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(glassteeth.Startup))]
namespace glassteeth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
