using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheaBlog.Startup))]
namespace TheaBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
