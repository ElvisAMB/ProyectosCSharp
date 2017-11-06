using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcwef.Startup))]
namespace mvcwef
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
