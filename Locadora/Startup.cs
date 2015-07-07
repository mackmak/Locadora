using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Locadora.Startup))]
namespace Locadora
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
