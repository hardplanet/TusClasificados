using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TusClasificados.Site.Startup))]
namespace TusClasificados.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
