using System.Web;
using System.Web.Http;

namespace MyUniversity.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.Run();
        }
    }
}
