using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Journal.WebMVC.Startup))]
namespace Journal.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
