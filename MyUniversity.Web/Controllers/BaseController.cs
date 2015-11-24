using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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
            Client.DefaultRequestHeaders.Add("contentType", "application/json; charset=utf-8");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResult> GetHttpResponMessageResultAsyc<TResult>(string requestUri)
        {
            var response = await Client.GetAsync(requestUri);
            var result = response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<TResult>()
                : default(TResult);
            return result;
        }

        public async Task<string> PostJsonAsyc(string requestUri, object value)
        {
            var response = await Client.PostAsJsonAsync(requestUri, value);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            throw new HttpException((int) response.StatusCode, "Error");
        }

        public async Task<string> PutJsonAsyc(string requestUri, object value)
        {
            var response = await Client.PutAsJsonAsync(requestUri, value);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            throw new HttpException((int)response.StatusCode, "Error");
        }
    }
}