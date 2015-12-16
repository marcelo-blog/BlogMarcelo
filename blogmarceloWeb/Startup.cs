using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(blogmarceloWeb.Startup))]
namespace blogmarceloWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
