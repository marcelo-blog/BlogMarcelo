using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogMarcelo.Startup))]
namespace BlogMarcelo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
