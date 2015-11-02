using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyUniversity.Web.Startup))]
namespace MyUniversity.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
