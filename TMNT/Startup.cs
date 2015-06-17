using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TMNT.Startup))]
namespace TMNT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
