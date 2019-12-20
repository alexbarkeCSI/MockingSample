using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MockingSample.Startup))]
namespace MockingSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
