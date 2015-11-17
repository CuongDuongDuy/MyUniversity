using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MyUniversity.Web.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient Client { get; set; }
        public BaseController()
        {
            Client = new HttpClient {BaseAddress = new Uri(WebConfigurationManager.AppSettings.Get("ApiBaseAddress"))};
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("contentType", "application/xml; charset=utf-8");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }

    }
}